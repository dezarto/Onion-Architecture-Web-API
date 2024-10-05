using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;

namespace DezartoAPI.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(CustomerDTO customerDto)
        {
            // Token oluşturma mantığı
            // Örnek: JWT kullanarak token oluşturulabilir
            return "GeneratedJWTToken"; // Burada gerçek bir JWT token oluşturmalısınız.
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
