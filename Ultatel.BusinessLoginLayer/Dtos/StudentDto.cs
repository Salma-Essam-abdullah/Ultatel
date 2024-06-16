using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class StudentDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; }

        public string AppUserId { get; set; }
    }
}
