
using System.ComponentModel.DataAnnotations;


namespace Ultatel.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
