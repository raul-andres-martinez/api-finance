using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Dtos;

namespace Finance.API.Controllers
{
    [Route("/api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
        {
            var response = await _userService.AddUserAsync(request);

            if (response)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _userService.LoginAsync(request);

            if (!response.Success) 
            {
                return StatusCode(response.ErrorCode.ToStatusCode(), response);
            }

            return Ok(response);
        }
    }
}
