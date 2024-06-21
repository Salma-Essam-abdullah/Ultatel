using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentLogsRepository
    {

        Task<IEnumerable<StudentLogs>> GetStudentLogs(int studentId);
    }
}
