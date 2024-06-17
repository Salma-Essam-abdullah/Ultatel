using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;
using static Ultatel.BusinessLoginLayer.Errors.ErrorMsg;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISecurityService _securityService;


        public UserService(UserManager<AppUser> userManager, ISecurityService securityService)
        {
            _userManager = userManager;
            _securityService = securityService;
        }

        public async Task<Response> RegisterUserAsync(RegisterDto model)
        {
            try
            {
                if (model == null)
                {
                    throw new NullReferenceException(ErrorMsg.NullModel);
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return new Response
                    {
                        Message = Msg.ConfirmPasswordNotMatch,
                        isSucceeded = false,
                    };
                }

                var user = new AppUser
                {
                    Email = model.Email,
                   
                    UserName = model.FullName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var confirmedEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmedEmailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                    // Send confirmation email (not shown)
                    return new Response
                    {
                        Message = Msg.UserCreated,
                        isSucceeded = true,
                    };
                }
                else
                {
                    return new Response
                    {
                        Message = "User registration failed",
                        isSucceeded = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    isSucceeded = false,
                    Errors = new[] { ex.InnerException?.Message ?? ex.Message }
                };
            }
        }

        public async Task<Response> LoginUserAsync(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new Response
                    {
                        Message = ErrorMsg.NoUserEmail,
                        isSucceeded = false
                    };
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return new Response
                    {
                        Message = ErrorMsg.InvalidPassword,
                        isSucceeded = false
                    };
                }

                var claims = new List<Claim>
                {
                    new Claim("Email", model.Email),
                    new Claim("UserId", user.Id),
                    new Claim("UserName", user.UserName)
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    if (role == "admin")
                    {
                        claims.Add(new Claim("IsAdmin", "true"));
                    }
                }

                if (!claims.Any(c => c.Type == "IsAdmin"))
                {
                    claims.Add(new Claim("IsAdmin", "false"));
                }

                // Security token generation
                _securityService.SecureToken(claims, out JwtSecurityToken token, out string tokenString);
                return new Response
                {
                    Message = tokenString,
                    isSucceeded = true,
                    ExpireDate = token.ValidTo
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    isSucceeded = false,
                };
            }
        }




    

}
}
