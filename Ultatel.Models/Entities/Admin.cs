
using Ultatel.Models.Entities.Identity;

namespace Ultatel.Models.Entities
{
    public class Admin :BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        public ICollection<Student> Students { get; set; }
    }
}
