using EMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EMS.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userName = User.Identity.Name; // Get logged-in Name
            var employeeDetails =  _context.Employees.FirstOrDefault(e => userName.Contains(e.Name));
            return View(employeeDetails);//Display the logged user information
        }
    }
}
