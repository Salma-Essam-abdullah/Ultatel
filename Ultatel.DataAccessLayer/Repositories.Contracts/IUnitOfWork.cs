using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Admin> _adminRepository { get; set; }

        public IGenericRepository<Student> _studentRepository { get; set; }

        public IStudentRepository _studentRepositoryN { get; set; }
        public IGenericRepository<StudentLogs> _studentLogsRepository { get; set; }

        public IStudentLogsRepository _studentLogsRepositoryN { get; set; }



    }
}
