
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultatel.Models.Entities
{
    public class StudentLogs:BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("UpdateAdmin")]
        public Guid? UpdateAdminId { get; set; }
        public Admin UpdateAdmin { get; set; }

        [ForeignKey("CreateAdmin")]
        public Guid? CreateAdminId { get; set; }
        public Admin CreateAdmin { get; set; }

        public DateTime? UpdateTimeStamps { get; set; }
        public DateTime? CreateTimeStamps { get; set; }



    }


}
