using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Services.Contracts;
using Ultatel.Models.Entities.Identity;
using Ultatel.Models.Entities;
using Ultatel.BusinessLoginLayer.Services;
using Ultatel.BusinessLoginLayer.Responses;

namespace Ultatel.Api.Controllers
{

    public class AuthenticationController : BaseController
    {

        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService  ,IAdminService adminService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _adminService = adminService;
            _logger = logger;
            


        }
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        Message = "Invalid properties",
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = await _userService.RegisterUserAsync(model);

                if (result.isSucceeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user");
                return StatusCode(500, new Response
                {
                    Message = "An unexpected error occurred. Please try again later.",
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        Message = "Invalid properties",
                        isSucceeded = false,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = await _adminService.RegisterAdminAsync(model);

                if (result.isSucceeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the admin");
                return StatusCode(500, new Response
                {
                    Message = "An unexpected error occurred. Please try again later.",
                    isSucceeded = false,
                    Errors = new[] { ex.Message }
                });
            }
        }



        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.LoginUserAsync(model);
                    if (result.isSucceeded)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result);
                }
                return BadRequest(ErrorMsg.InvalidProperties); 

            }catch(Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }
    }
}
