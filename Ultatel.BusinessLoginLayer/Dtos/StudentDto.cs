using System.ComponentModel.DataAnnotations;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class EnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string stringValue)
            {
                return Enum.TryParse(_enumType, stringValue, true, out _);
            }

            return Enum.IsDefined(_enumType, value);
        }
    }

    public class StudentDto
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [EnumValidation(typeof(Gender), ErrorMessage = "Invalid gender value.")]
        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; }




    }
}
