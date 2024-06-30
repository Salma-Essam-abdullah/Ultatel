using Microsoft.EntityFrameworkCore;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories
{
    public class StudentsLogsRepository : IStudentLogsRepository
    {
        private readonly UltatelDbContext _context;

        private readonly DbSet<StudentLogs> _dbSet;
        public StudentsLogsRepository(UltatelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<StudentLogs>();

        }
        public async Task<IEnumerable<StudentLogs>> GetStudentLogs(Guid studentId)
        {
            return await _dbSet
        .Where(s => s.StudentId == studentId)
        .Include(s => s.UpdateAdmin)
            .ThenInclude(ua => ua.AppUser)
        .Include(s => s.CreateAdmin)
            .ThenInclude(ca => ca.AppUser)
        .ToListAsync();
        }
    }
}
