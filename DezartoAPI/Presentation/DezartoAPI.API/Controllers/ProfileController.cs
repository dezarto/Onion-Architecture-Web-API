using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DezartoAPI.API.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public ProfileController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _customerAppService.GetByCustomerEmailAsync(userEmail);

            if (customer == null)
            {
                return NotFound("User not found.");
            }

            return Ok(customer);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _customerAppService.GetByCustomerEmailAsync(userEmail);

            if (customer == null)
            {
                return NotFound("User not found.");
            }

            customer.Name = updateProfileDto.Name ?? customer.Name;
            customer.Surname = updateProfileDto.Surname ?? customer.Surname;
            customer.Gender = updateProfileDto.Gender ?? customer.Gender;
            customer.DateOfBirth = updateProfileDto?.DateOfBirth ?? customer.DateOfBirth;
            customer.PhoneNumber = updateProfileDto?.PhoneNumber ?? customer.PhoneNumber;
            customer.UpdatedDate = DateTime.UtcNow;

            await _customerAppService.UpdateCustomerAsync(customer);

            return Ok(customer);
        }
    }
}
