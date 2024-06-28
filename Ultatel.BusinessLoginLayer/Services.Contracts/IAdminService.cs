using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IAdminService
    {
        Task<ValidationResponse> RegisterSuperAdminAsync(RegisterDto model);

        Task<ValidationResponse> RegisterAdminAsync(RegisterDto model);

        Task<AdminDto> GetAdminByUserId(string userId);

    }
}
