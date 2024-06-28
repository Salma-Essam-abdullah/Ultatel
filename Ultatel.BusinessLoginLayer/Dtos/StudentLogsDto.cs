

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class StudentLogsDto
    {
        public string Operation { get; set; } // e.g., "Created", "Updated"
        public DateTime OperationTime { get; set; }
        public Guid StudentId { get; set; }

        public Guid AdminId { get; set; }

        public string UserName { get; set; }

    }
}
