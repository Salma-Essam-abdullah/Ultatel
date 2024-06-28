using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IAdminService
    {
        Task<Response> RegisterSuperAdminAsync(RegisterDto model);

        Task<Response> RegisterAdminAsync(RegisterDto model);

        Task<AdminDto> GetAdminByUserId(string userId);

    }
}
