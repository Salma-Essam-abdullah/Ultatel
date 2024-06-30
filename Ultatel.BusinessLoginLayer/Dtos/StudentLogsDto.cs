

namespace Ultatel.BusinessLoginLayer.Dtos
{
    public class StudentLogsDto
    {
        public Guid StudentId { get; set; }
        public Guid? UpdateAdminId { get; set; }

        public Guid? CreateAdminId { get; set; }

        public DateTime? UpdateTimeStamps { get; set; }
        public DateTime? CreateTimeStamps { get; set; }

        public string UpdateUserName { get; set; }

        public string CreateUserName { get; set; }

    }
}
