using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Ultatel.BusinessLoginLayer.Dtos;

namespace Ultatel.BusinessLoginLayer.Errors
{
    public class RegisterUserValidator
    {

        public Dictionary<string, string> Validate(RegisterDto registerDto)
        {
            var errors = new Dictionary<string, string>();

            // Validate UserName
            if (string.IsNullOrWhiteSpace(registerDto.UserName))
            {
                errors["userName"] = "UserName is required.";
            }
            else if (registerDto.UserName.Length > 50)
            {
                errors["userName"] = "UserName cannot be longer than 50 characters.";
            }
            else if (registerDto.UserName.Contains(" "))
            {
                errors["userName"] = "UserName cannot contain spaces.";
            }
          

            // Validate Email
            if (string.IsNullOrWhiteSpace(registerDto.Email))
            {
                errors["email"] = "Email is required.";
            }
            else if (registerDto.Email.Length > 50)
            {
                errors["email"] = "Email cannot be longer than 50 characters.";
            }
            else if (!new EmailAddressAttribute().IsValid(registerDto.Email))
            {
                errors["email"] = "Invalid email format.";
            }


            // Validate Password
            if (string.IsNullOrWhiteSpace(registerDto.Password))
            {
                errors["password"] = "Password is required.";
            }
            else if (registerDto.Password.Length < 8 || registerDto.Password.Length > 50)
            {
                errors["password"] = "Password must be between 8 and 50 characters.";
            }
            else if (!IsValidPassword(registerDto.Password))
            {
                errors["password"] = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
            }

            // Validate ConfirmPassword
            if (string.IsNullOrWhiteSpace(registerDto.ConfirmPassword))
            {
                errors["confirmPassword"] = "ConfirmPassword is required.";
            }
            else if (registerDto.ConfirmPassword != registerDto.Password)
            {
                errors["confirmPassword"] = "ConfirmPassword must match Password.";
            }

            return errors;

        }
        private bool IsValidPassword(string password)
        {
            // Regular expression to validate the password
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }

}
