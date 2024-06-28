using Microsoft.AspNetCore.Mvc;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.BusinessLoginLayer.Errors;
using Ultatel.BusinessLoginLayer.Services.Contracts;
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
        public async Task<ActionResult> RegisterAdmin(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Message = "InvalidProperties",
                    isSucceeded = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToDictionary(error => error, error => error)
                });
            }

            var result = await _adminService.RegisterAdminAsync(model);

            if (result.isSucceeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("RegisterSuperAdmin")]
        public async Task<ActionResult> RegisterSuperAdmin(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Message = "InvalidProperties",
                    isSucceeded = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToDictionary(error => error, error => error)
                });
            }

            var result = await _adminService.RegisterSuperAdminAsync(model);

            if (result.isSucceeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    Message = "InvalidProperties",
                    isSucceeded = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToDictionary(error => error, error => error)
                });
            }

            var result = await _userService.LoginUserAsync(model);
            if (result.isSucceeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



    }
}
