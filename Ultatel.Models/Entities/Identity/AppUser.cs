using Microsoft.AspNetCore.Identity;
namespace Ultatel.Models.Entities.Identity
{
  
   
    public class AppUser : IdentityUser
    {
        public string fullName { get; set; }
    }
}
