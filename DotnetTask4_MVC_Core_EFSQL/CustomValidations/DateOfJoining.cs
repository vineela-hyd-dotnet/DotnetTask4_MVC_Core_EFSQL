using System.ComponentModel.DataAnnotations;

namespace DotnetTask4_MVC_Core_EFSQL.Validations

{

    public class DateOfJoining : ValidationAttribute

    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            if (value == null)

            {

                return new ValidationResult("Date of joining is required.");

            }

            if (value is DateTime dateOfJoining)

            {

                if (dateOfJoining > DateTime.Now)

                    return new ValidationResult("Date of joining cannot be in the future.");

                if (dateOfJoining.DayOfWeek == DayOfWeek.Saturday || dateOfJoining.DayOfWeek == DayOfWeek.Sunday)

                    return new ValidationResult("Date of joining cannot be on a weekend.");

                return ValidationResult.Success;

            }

            return new ValidationResult("Invalid date format.");

        }

    }

}

