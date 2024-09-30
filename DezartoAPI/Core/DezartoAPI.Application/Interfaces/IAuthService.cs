using DezartoAPI.Application.DTOs;

namespace DezartoAPI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResult> LoginAsync(LoginDTO loginDto);
    }
}
