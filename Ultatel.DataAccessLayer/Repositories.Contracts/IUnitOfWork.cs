
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<SuperAdmin> _superAdminRepository { get; set; }


        public IGenericRepository<Admin> _adminRepository { get; set; }
        public IGenericRepository<Student> _studentRepository { get; set; }

        public IStudentRepository _studentRepositoryNonGeneric { get; set; }
        public IGenericRepository<StudentLogs> _studentLogsRepository { get; set; }

        public IStudentLogsRepository _studentLogsRepositoryNonGeneric { get; set; }

        public IAdminRepository _adminRepositoryNonGeneric { get; set; }



    }
}
