using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LoginUserValidator _loginValidator;



        public UserService(UserManager<AppUser> userManager, ISecurityService securityService, IUnitOfWork unitOfWork, LoginUserValidator loginValidator)
        {
            _userManager = userManager;
            _securityService = securityService;
            _loginValidator = loginValidator;
            _unitOfWork = unitOfWork;
        }



        public async Task<ValidationResponse> LoginUserAsync(LoginDto model)
        {
            var validationErrors = _loginValidator.Validate(model);
            if (validationErrors.Count > 0)
            {
                return new ValidationResponse
                {
                    Message = "ValidationError",
                    isSucceeded = false,
                    Errors = validationErrors
                };
            }

            var errors = new Dictionary<string, string>();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                errors.Add("Email", "No user found with this email.");
            }
            else
            {
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    errors.Add("Password", "Invalid password.");
                }
            }

            if (errors.Any())
            {
                return new ValidationResponse
                {
                    Message = "LoginFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            var claims = new List<Claim>
            {
                new Claim("Email", model.Email),
                new Claim("UserId", user.Id),
                new Claim("UserName", user.UserName),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            var admin = await _unitOfWork._adminRepositoryNonGeneric.GetAdminByUserId(user.Id);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                if (role == "Admin" && admin != null)
                {
                    claims.Add(new Claim("UserGuid", admin.Id.ToString(), ClaimValueTypes.String));
                    claims.Add(new Claim("IsSuperAdmin", "false"));
                }
                if (role == "superAdmin")
                {
                    claims.Add(new Claim("IsSuperAdmin", "true"));
                }
            }

            _securityService.SecureToken(claims, out JwtSecurityToken token, out string tokenString);
            return new ValidationResponse
            {
                Message = tokenString,
                isSucceeded = true,
             
            };
        }
    }

}
