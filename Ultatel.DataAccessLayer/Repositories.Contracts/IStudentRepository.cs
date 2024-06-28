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
        Task<IEnumerable<Student>> GetStudentsByAdminIdAsync(Guid adminId, int pageIndex, int pageSize);
        Task<IEnumerable<Student>> SearchStudentsAsync(string name, int? ageFrom, int? ageTo, string country, Gender? gender);

        Task<bool> AnyAsync(Expression<Func<Student, bool>> predicate);
    }
}
