using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<StudentLogs>> GetStudentLogs(int studentId)
        {
            return await _dbSet
               .Where(s => s.StudentId == studentId).Include(a => a.AppUser).ToListAsync();
        }
    }
}
