using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface IUserService
    {
         Task<Response> RegisterUserAsync(RegisterDto model);

        Task<Response> LoginUserAsync(LoginDto model);
    }
}
