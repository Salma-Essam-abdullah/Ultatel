
using System.ComponentModel.DataAnnotations;


namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class RegisterDto
    {

        [Required]
        [StringLength(20)]
        [MinLength(3)]


        public string fullName { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(3)]

        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}
