using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ultatel.BusinessLoginLayer.Services.Contracts
{
    public interface ISecurityService
    {
        void SecureToken(List<Claim> claim, out JwtSecurityToken token, out string TokenString);
    }
}
