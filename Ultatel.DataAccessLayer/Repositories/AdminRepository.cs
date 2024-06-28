using Microsoft.EntityFrameworkCore;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        private readonly UltatelDbContext _context;

        private readonly DbSet<Admin> _dbSet;
        public AdminRepository(UltatelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Admin>();

        }
        public async Task<Admin> GetAdminByUserId(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            else
            {
                return await _dbSet
                .Where(s => s.AppUser.Id == userId).FirstOrDefaultAsync();
            }
        }

     
    }
}
