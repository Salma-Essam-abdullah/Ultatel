
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentLogsRepository
    {

        Task<IEnumerable<StudentLogs>> GetStudentLogs(Guid studentId);
    }
}
