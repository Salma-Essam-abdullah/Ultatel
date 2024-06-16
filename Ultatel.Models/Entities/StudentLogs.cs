using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.Models.Entities
{
    public class StudentLogs:BaseEntity
    {
     
        public string Operation { get; set; } // e.g., "Created", "Updated", "Deleted" 
        public DateTime OperationTime { get; set; }
        public int StudentId { get; set; }


        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public Student Student { get; set; }
        
    }

    
}
