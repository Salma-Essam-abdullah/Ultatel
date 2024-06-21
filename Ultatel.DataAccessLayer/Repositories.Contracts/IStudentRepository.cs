using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<int> CountAsyncByUserId(string userId);
        Task<IEnumerable<Student>> GetStudentsByUserIdAsync(string userId, int pageIndex, int pageSize);
        Task<IEnumerable<Student>> SearchStudentsAsync(string name, int? ageFrom, int? ageTo, string country, Gender? gender);
    }
}
