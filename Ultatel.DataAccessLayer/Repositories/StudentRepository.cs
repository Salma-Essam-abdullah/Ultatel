using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
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
        public async Task<IEnumerable<Student>> GetStudentsByAdminIdAsync(Guid adminId, int pageIndex, int pageSize)
        {
            return await _context.Students
                .Where(s => s.AdminId == adminId).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsyncByUserId(Guid adminId)
        {
            return await _context.Students
                .Where(s => s.AdminId == adminId).CountAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Set<Student>().AnyAsync(predicate);
        }



        public async Task<IEnumerable<Student>> SearchStudentsAsync(string name, int? ageFrom, int? ageTo, string country, Gender? gender)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.FirstName.Contains(name) || s.LastName.Contains(name));
            }

            if (ageFrom.HasValue)
            {
                var ageFromDate = DateTime.Today.AddYears(-ageFrom.Value);
                query = query.Where(s => s.BirthDate <= ageFromDate);
            }

            if (ageTo.HasValue)
            {
                var ageToDate = DateTime.Today.AddYears(-ageTo.Value - 1).AddDays(1);
                query = query.Where(s => s.BirthDate >= ageToDate);
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(s => s.Country == country);
            }

            if (gender.HasValue)
            {
                query = query.Where(s => s.Gender == gender.Value);
            }

            return await query.ToListAsync();
        }





    }
}
