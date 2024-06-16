using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.Models.Entities
{
    public class Admin :BaseEntity
    {
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }


    }
}
