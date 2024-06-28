
using Ultatel.Models.Entities.Identity;

namespace Ultatel.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
