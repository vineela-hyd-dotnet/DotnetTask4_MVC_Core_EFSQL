using DotnetTask4_MVC_Core_EFQL.Validations;
using DotnetTask4_MVC_Core_EFSQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetTask4_MVC_Core_EFQL.Models
    
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceId { get; set; }

        [ForeignKey("Employee")]
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeId { get; set; }
        [ValidateNever]
        public Employee Employee { get; set; }
        [UniqueAttendance]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }

        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }
        


    }

}
