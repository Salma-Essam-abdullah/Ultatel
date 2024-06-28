using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IUserService
    {
        Task<ValidationResponse> LoginUserAsync(LoginDto model);
    }
}
