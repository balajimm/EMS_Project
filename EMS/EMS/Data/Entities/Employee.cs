using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EMS.Data.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(250)]
        [NotNull]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The field must contain letters only.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(150)]
        [NotNull]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The field must contain letters only.")]
        public string Department { get; set; }

        // [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "The field must contain letters only.")]
        [Required(ErrorMessage = "Job Title is required.")]
        [StringLength(250)]
        [NotNull]      
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Column(TypeName = "decimal(18,2)")] // Adjust as needed
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        [NotNull]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Remote Work Statsu should be selected.")]
        [StringLength(50)]
        public string RemoteWorkStatus { get; set; }
    }
}
