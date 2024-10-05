using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;

namespace DezartoAPI.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(CustomerDTO customerDto)
        {
            // Token oluşturma mantığı
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // appsettings.json'dan alınan gizli anahtar
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, customerDto.Email), // Email veya diğer kullanıcı bilgileri
                new Claim(ClaimTypes.NameIdentifier, customerDto.Id.ToString()) // Kullanıcı ID'si
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1), // Token geçerlilik süresi
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Oluşturulan token'ı döndür
        }

        public RefreshToken GenerateRefreshToken()
        {
            // Refresh token oluşturma mantığı
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7) // Örneğin, 7 gün geçerli
            };
        }
    }
}
