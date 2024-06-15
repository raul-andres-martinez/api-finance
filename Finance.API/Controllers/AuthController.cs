using Finance.Domain.Dtos.Requests;
using Finance.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest request)
        {
            var response = await _userService.AddUserAsync(request);

            if (response)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
