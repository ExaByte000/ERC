using System.ComponentModel.DataAnnotations;

namespace AccountService.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return true; // Null values are handled by Required attribute
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Поле {name} должно содержать будущую дату";
        }
    }
}
