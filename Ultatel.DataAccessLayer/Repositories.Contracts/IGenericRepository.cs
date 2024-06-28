
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
     
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);

        Task<int> CountAsync();
      
    }

}
