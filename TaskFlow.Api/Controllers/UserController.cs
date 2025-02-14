using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Models;
using TaskFlow.Api.Services.Interfaces;

namespace TaskFlow.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(model.Username, model.Password);
            if (result)
                return Ok("User registered successfully.");

            return BadRequest("User registration failed.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.LoginAsync(model.Username, model.Password);
            if (token != null)
                return Ok(new { Token = token });

            return Unauthorized("Invalid username or password.");
        }

        [Authorize]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleAssignmentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AssignRoleAsync(model.Username, model.Role);
            if (result)
                return Ok("Role assigned successfully.");

            return BadRequest("Role assignment failed.");
        }
    }
}
