using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<int> CountAsyncByUserId(Guid adminId);

        Task<IEnumerable<Student>> SearchStudentsAsync(string name, int? ageFrom, int? ageTo, string country, Gender? gender, int pageIndex, int pageSize, string? sortBy, bool isDescending);
        Task<bool> AnyAsync(Expression<Func<Student, bool>> predicate);

        Task<IEnumerable<Student>> GetAllStudentsAsync(int pageIndex, int pageSize, string? sortBy, bool isDescending);

       
    }
}   
