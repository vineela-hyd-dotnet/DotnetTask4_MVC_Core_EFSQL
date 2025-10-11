using System;
using DotnetTask4_MVC_Core_EFSQL.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTask4_MVC_Core_EFSQL.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Designation must be between 2 and 50 characters")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Department must be between 2 and 50 characters")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Date of joining is required")]
        [DataType(DataType.Date)]
        [DateOfJoining]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}