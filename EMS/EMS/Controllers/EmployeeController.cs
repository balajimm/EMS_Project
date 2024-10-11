using EMS.Data;
using EMS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMS.Controllers
{
    [Authorize(Roles = "Employee, Admin")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Check for existing employee with the same Name
                if (EmployeeExistsByName(employee.Name))
                {
                    //If the employee name already exist, return error message as name already exist 
                    ModelState.AddModelError("Name", "An employee with this name already exists.");
                    return View(employee);
                }
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        // POST: Employee/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var employee = _context.Employees.Find(id);
                    if (employee == null)
                    {
                        return NotFound(); // Return 404 if not found
                    }

                    _context.Employees.Remove(employee); // Remove the employee
                    _context.SaveChanges(); // Save changes to the database
                    transaction.CommitAsync();

                }
                catch (Exception)
                {
                    transaction.RollbackAsync();
                    return Json(new { success = false, message = "An error occurred while saving the employee." });
                }
                
            }
            return Json(new { success = true }); // Return a JSON response
        }
        // GET: Employee/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        /// <summary>
        /// Check Employee Exist by the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
        /// <summary>
        /// Check Employee Exist by the given Name
        /// </summary>
        /// <param name="empName"></param>
        /// <returns>bool</returns>
        private bool EmployeeExistsByName(string empName)
        {
            return _context.Employees.Any(e => e.Name.ToLower() == empName.ToLower());
        }
    }
}
