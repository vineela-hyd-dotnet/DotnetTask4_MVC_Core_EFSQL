using DotnetTask4_MVC_Core_EFQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotnetTask4_MVC_Core_EFQL.Validations
{
    public class UniqueAttendance : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is required.");

            var attendance = (Attendance)validationContext.ObjectInstance;
            var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

            if (dbContext == null)
                return new ValidationResult("Database context is not available.");

            bool exists = dbContext.Attendances
                .Any(a => a.EmployeeId == attendance.EmployeeId
                       && a.Date.Date == attendance.Date.Date
                       && a.AttendanceId != attendance.AttendanceId); // ✅ exclude current record

            if (exists)
            {
                return new ValidationResult("Attendance for this employee on this date already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
