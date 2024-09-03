using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Utils.Result;
using System.ComponentModel.DataAnnotations;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult> Register([FromBody][Required] UserRequest request)
        {
            return await _userService.AddUserAsync(request);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomActionResult<LoginResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return await _userService.LoginAsync(request);
        }
    }
}
