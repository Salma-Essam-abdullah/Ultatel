using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<SuperAdmin> _superAdminRepository { get; set; }

        public IGenericRepository<Admin> _adminRepository { get; set; }
        public IGenericRepository<Student> _studentRepository { get; set; }

        public IStudentRepository _studentRepositoryNonGeneric { get; set; }
        public IGenericRepository<StudentLogs> _studentLogsRepository { get; set; }

        public IStudentLogsRepository _studentLogsRepositoryNonGeneric { get; set; }
        public IAdminRepository _adminRepositoryNonGeneric { get; set; }

        public UnitOfWork(IGenericRepository<SuperAdmin> superAdminRepository, IGenericRepository<Student> studentRepository, IStudentRepository studentRepositoryNonGeneric, IGenericRepository<StudentLogs> studentLogsRepository, IStudentLogsRepository studentLogsRepositoryNonGeneric, IGenericRepository<Admin> adminRepository, IAdminRepository adminRepositoryNonGeneric)
        {
            _superAdminRepository = superAdminRepository;
            _studentRepository = studentRepository;
            _studentRepositoryNonGeneric = studentRepositoryNonGeneric;
            _studentLogsRepository = studentLogsRepository;
            _studentLogsRepositoryNonGeneric = studentLogsRepositoryNonGeneric;
            _adminRepository = adminRepository;
            _adminRepositoryNonGeneric = adminRepositoryNonGeneric;
        }
    }
}
