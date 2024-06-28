using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Errors
{
    public class UpdateStudentValidator
    {
        public Dictionary<string, string> Validate(UpdateStudentDto student)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                errors["name"] = "Student first name can't be empty";
            }
            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                errors["name"] = "Student last name can't be empty";
            }

            if (string.IsNullOrWhiteSpace(student.Email))
            {
                errors["email"] = "Email can't be empty";
            }
            else if (!new EmailAddressAttribute().IsValid(student.Email))
            {
                errors["email"] = "Invalid email format";
            }


            if (string.IsNullOrWhiteSpace(student.Gender.ToString()))
            {
                errors["gender"] = "Gender can't be empty";
            }
            else if (student.Gender != Gender.Male && student.Gender != Gender.Female)
            {
                errors["gender"] = "Gender must be Male or Female";
            }


            if (student.BirthDate > DateTime.Now.AddYears(-4))
            {
                errors["birthDate"] = "BirthDate must be at least 4 years ago";
            }

            if (string.IsNullOrWhiteSpace(student.Country))
            {
                errors["country"] = "Country can't be empty";
            }

            return errors;
        }
    }
}
