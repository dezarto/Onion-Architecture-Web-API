using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DezartoAPI.API.Controllers
{
    [Authorize]
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
            // Token'dan kullanıcının ID'sini veya e-mail'ini çıkarıyoruz
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized(); // Eğer token geçerli değilse
            }

            // Kullanıcının e-mail'ine göre veritabanından profil bilgilerini çekiyoruz
            var customer = await _customerAppService.GetByEmailAsync(userEmail);

            if (customer == null)
            {
                return NotFound("User not found.");
            }

            // Profil bilgilerini döndür
            return Ok(customer);
        }
    }
}
