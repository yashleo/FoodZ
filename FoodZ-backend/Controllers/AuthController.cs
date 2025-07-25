using Foodz.API.DTOs.Auth;
using Foodz.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foodz.API.Controllers
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

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return Ok(response);
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }
    }
}
