using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IUserService
    {
        Task<Response> LoginUserAsync(LoginDto model);
    }
}
