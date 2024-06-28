using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;

namespace Ultatel.BusinessLoginLayer.Errors
{
    public class LoginUserValidator
    {
        public Dictionary<string, string> Validate(LoginDto loginDto)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(loginDto.Email))
            {
                errors["email"] = "Email is required.";
            }
            else if (loginDto.Email.Length > 50)
            {
                errors["email"] = "Email cannot be longer than 50 characters.";
            }
            else if (!new EmailAddressAttribute().IsValid(loginDto.Email))
            {
                errors["email"] = "Invalid email format.";
            }


            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                errors["password"] = "Password is required.";
            }
            else if (loginDto.Password.Length < 8 || loginDto.Password.Length > 50)
            {
                errors["password"] = "Password must be between 8 and 50 characters.";
            }
            else if (!IsValidPassword(loginDto.Password))
            {
                errors["password"] = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";
            }

            return errors;

        }
        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}

