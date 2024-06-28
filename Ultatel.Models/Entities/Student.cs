using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Admin> Admins { get; set; } = new List<Admin>();





    }

}
