using DezartoAPI.Application.DTOs;
using DezartoAPI.Domain.Entities;

namespace DezartoAPI.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(CustomerDTO customerDto);
        RefreshToken GenerateRefreshToken();
    }
}
