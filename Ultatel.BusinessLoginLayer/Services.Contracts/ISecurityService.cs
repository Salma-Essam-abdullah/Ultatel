using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface ISecurityService
    {
        void SecureToken(List<Claim> claim, out JwtSecurityToken token, out string TokenString);
    }
}
