
using System.ComponentModel.DataAnnotations;

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50 , MinimumLength =8)]
        public string Password { get; set; }
    }
}
