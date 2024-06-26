﻿using System.ComponentModel.DataAnnotations;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Dtos
{
  
    public class UpdateStudentDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }


        [EnumValidation(typeof(Gender), ErrorMessage = "Invalid gender value.")]
        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Country { get; set; }

    }
}
