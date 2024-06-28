
using Ultatel.Models.Entities;

namespace Ultatel.DataAccessLayer.Repositories.Contracts
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByUserId(string userId);
    }
}
