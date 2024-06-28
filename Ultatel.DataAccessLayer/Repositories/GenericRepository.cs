using Microsoft.EntityFrameworkCore;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly UltatelDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(UltatelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

       
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
       

    }

}
