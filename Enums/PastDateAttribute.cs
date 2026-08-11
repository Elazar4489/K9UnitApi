using System.ComponentModel.DataAnnotations;
namespace K9UnitApi.Enums
{

    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime.Date < DateTime.Today)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("The date must be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}