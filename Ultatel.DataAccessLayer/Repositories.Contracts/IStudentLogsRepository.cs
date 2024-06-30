
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentLogsRepository
    {

        Task<StudentLogs> GetStudentLogsById(Guid studentId);

    }
}
