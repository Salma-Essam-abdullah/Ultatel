using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities.Identity;
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class StudentLogsDto
    {
        public string Operation { get; set; } // e.g., "Created", "Updated"
        public DateTime OperationTime { get; set; }
        public int StudentId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

    }
}
