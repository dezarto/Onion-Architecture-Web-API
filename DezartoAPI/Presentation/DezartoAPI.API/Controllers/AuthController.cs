using Microsoft.AspNetCore.Mvc;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Application.DTOs;
using System.Threading.Tasks;

namespace DezartoAPI.API.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerDTO customerDto)
        {
            var result = await _authService.RegisterAsync(customerDto);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }
    }
}
