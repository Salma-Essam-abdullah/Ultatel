
using Ultatel.Models.Entities;

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class StudentSearchDto
    {
        public string? Name { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string? Country { get; set; }
        public Gender? Gender { get; set; }
    }
}
