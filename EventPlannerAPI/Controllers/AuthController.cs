using Microsoft.AspNetCore.Mvc;
using EventPlannerAPI.Models.DTOs;
using EventPlannerAPI.Services;

namespace EventPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username))
            {
                return BadRequest("Username is required");
            }

            var user = await _authService.LoginAsync(loginDto.Username);
            return Ok(new { username = user.Username, message = "Login successful" });
        }

        [HttpGet("validate/{username}")]
        public async Task<IActionResult> ValidateUser(string username)
        {
            var isValid = await _authService.ValidateUserAsync(username);
            return Ok(new { isValid, username });
        }
    }
}