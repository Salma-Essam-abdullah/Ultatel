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
    public class StudentRepository :IStudentRepository
    {
        private readonly UltatelDbContext _context;

        private readonly DbSet<Student> _dbSet;
        public StudentRepository(UltatelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Student>();

        }
        public async Task<IEnumerable<Student>> GetStudentsByUserIdAsync(string userId, int pageIndex, int pageSize)
        {
            return await _context.Students
                .Where(s => s.AppUserId == userId).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsyncByUserId(string userId)
        {
            return await _context.Students
                .Where(s => s.AppUserId == userId).CountAsync();
        }


       
    }
}
