using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.Models.Entities
{
    public enum Gender
    {
        Male,
        Female
    }
    [Index(nameof(Email), IsUnique = true)]
    public class Student:BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Country { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }




       
    }

}
