using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RegisterUserValidator _validator;
        public AdminService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            RegisterUserValidator validator,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<ValidationResponse> RegisterSuperAdminAsync(RegisterDto model)
        {
            var validationErrors = _validator.Validate(model);
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

            if (model == null)
            {
                errors["Model"] = "NullModel";
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors["ConfirmPassword"] = "ConfirmPasswordNotMatch";
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                errors["Email"] = "Email already exists";
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);
            if (existingUserByUsername != null)
            {
                errors["Username"] = "Username already exists";
            }

            if (errors.Any())
            {
                return new ValidationResponse
                {
                    Message = "ValidationFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                return new ValidationResponse
                {
                    Message = "SuperAdminRegistrationFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            if (!await _roleManager.RoleExistsAsync("superAdmin"))
            {
                var role = new IdentityRole("superAdmin");
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    errors = roleResult.Errors.ToDictionary(e => e.Code, e => e.Description);
                    return new ValidationResponse
                    {
                        Message = "RoleCreationFailed",
                        isSucceeded = false,
                        Errors = errors
                    };
                }
            }

            var roleAssignResult = await _userManager.AddToRoleAsync(user, "superAdmin");
            if (!roleAssignResult.Succeeded)
            {
                errors = roleAssignResult.Errors.ToDictionary(e => e.Code, e => e.Description);
                return new ValidationResponse
                {
                    Message = "RoleAssignmentFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            var superAdminDto = new SuperAdminDto { AppUserId = user.Id };
            var superAdmin = _mapper.Map<SuperAdmin>(superAdminDto);
            await _unitOfWork._superAdminRepository.AddAsync(superAdmin);

            return new ValidationResponse
            {
                Message = "SuperAdminRegisteredSuccessfully",
                isSucceeded = true
            };
        }



        public async Task<ValidationResponse> RegisterAdminAsync(RegisterDto model)
        {
            var validationErrors = _validator.Validate(model);
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

            if (model == null)
            {
                errors["Model"] = "NullModel";
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors["ConfirmPassword"] = "ConfirmPasswordNotMatch";
            }

            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                errors["Email"] = "Email already exists";
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);
            if (existingUserByUsername != null)
            {
                errors["Username"] = "Username already exists";
            }

            if (errors.Any())
            {
                return new ValidationResponse
                {
                    Message = "ValidationFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                return new ValidationResponse
                {
                    Message = "AdminRegistrationFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    errors = roleResult.Errors.ToDictionary(e => e.Code, e => e.Description);
                    return new ValidationResponse
                    {
                        Message = "RoleCreationFailed",
                        isSucceeded = false,
                        Errors = errors
                    };
                }
            }

            var roleAssignResult = await _userManager.AddToRoleAsync(user, "Admin");
            if (!roleAssignResult.Succeeded)
            {
                errors = roleAssignResult.Errors.ToDictionary(e => e.Code, e => e.Description);
                return new ValidationResponse
                {
                    Message = "RoleAssignmentFailed",
                    isSucceeded = false,
                    Errors = errors
                };
            }

            var adminDto = new AdminDto { AppUserId = user.Id };
            var admin = _mapper.Map<Admin>(adminDto);
            await _unitOfWork._adminRepository.AddAsync(admin);

            return new ValidationResponse
            {
                Message = "AdminRegisteredSuccessfully",
                isSucceeded = true
            };
        }




        public async Task<AdminDto> GetAdminByUserId(string userId)
        {
            var admin =await _unitOfWork._adminRepositoryNonGeneric.GetAdminByUserId(userId);
            var adminDTO = _mapper.Map<AdminDto>(admin);
            return adminDTO;
        }


    }

}
