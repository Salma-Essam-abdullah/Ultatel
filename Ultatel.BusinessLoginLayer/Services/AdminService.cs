using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Responses;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.DataAccessLayer.Repositories.Contracts;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;
using static Ultatel.BusinessLoginLayer.Errors.ErrorMsg;

namespace Ultatel.BusinessLoginLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> RegisterAdminAsync(RegisterDto model)
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

                var user = _mapper.Map<AppUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return new Response
                    {
                        Message = "Admin registration failed",
                        isSucceeded = false,
                        Errors = result.Errors.Select(e => e.Description)
                    };
                }

                if (!await _roleManager.RoleExistsAsync("admin"))
                {
                    var role = new IdentityRole("admin");
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded)
                    {
                        return new Response
                        {
                            Message = "Role creation failed",
                            isSucceeded = false,
                            Errors = roleResult.Errors.Select(e => e.Description)
                        };
                    }
                }

                var roleAssignResult = await _userManager.AddToRoleAsync(user, "admin");
                if (!roleAssignResult.Succeeded)
                {
                    return new Response
                    {
                        Message = "Adding admin role failed",
                        isSucceeded = false,
                        Errors = roleAssignResult.Errors.Select(e => e.Description)
                    };
                }

                var adminDto = new AdminDto { AppUserId = user.Id };
                var admin = _mapper.Map<Admin>(adminDto);
                await _unitOfWork._adminRepository.AddAsync(admin);

                return new Response
                {
                    Message = "Admin registered successfully",
                    isSucceeded = true
                };
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


    }

}
