using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Services.Contracts;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;

        public SecurityService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void SecureToken(List<Claim> claims, out JwtSecurityToken token, out string tokenString)
        {
            var key = Convert.FromBase64String(_configuration["JWT:SecretKey"]);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["JWT:Duration"])),
                signingCredentials: credentials
            );

            tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}