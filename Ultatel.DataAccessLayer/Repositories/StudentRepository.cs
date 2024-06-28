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

    



        public async Task<int> CountAsyncByUserId(Guid adminId)
        {
            return await _context.Students.CountAsync();
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


       
        public async Task<IEnumerable<Student>> GetAllStudentsAsync( int pageIndex, int pageSize, string? sortBy, bool isDescending)
        {
            IQueryable<Student> query = _context.Students;

            switch (sortBy?.ToLower())
            {
                case "name":
                    query = isDescending ? query.OrderByDescending(s => s.FirstName + " " + s.LastName) : query.OrderBy(s => s.FirstName + " " + s.LastName);
                    break;
                case "country":
                    query = isDescending ? query.OrderByDescending(s => s.Country) : query.OrderBy(s => s.Country);
                    break;
                case "age":
                    query = isDescending ? query.OrderByDescending(s => EF.Functions.DateDiffYear(s.BirthDate, DateTime.Now)) : query.OrderBy(s => EF.Functions.DateDiffYear(s.BirthDate, DateTime.Now));
                    break;
                case "email":
                    query = isDescending ? query.OrderByDescending(s => s.Email) : query.OrderBy(s => s.Email);
                    break;
                case "gender":
                    query = isDescending ? query.OrderByDescending(s => s.Gender) : query.OrderBy(s => s.Gender);
                    break;
                default:
                    query = query.OrderBy(s => s.Id);
                    break;
            }

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(a=>a.Admins).ToListAsync();



        }




       

    }
}
