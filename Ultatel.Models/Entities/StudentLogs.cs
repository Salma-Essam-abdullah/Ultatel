
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultatel.Models.Entities
{
    public class StudentLogs:BaseEntity
    {
     
        public string Operation { get; set; } // e.g., "Created", "Updated", "Deleted" 
        public DateTime OperationTime { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Admin")]
        public Guid AdminId { get; set; }
        public Admin Admin { get; set; }

    }

    
}
