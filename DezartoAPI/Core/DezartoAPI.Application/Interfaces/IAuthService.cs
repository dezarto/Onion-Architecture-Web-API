using DezartoAPI.Application.DTOs;

namespace DezartoAPI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(CustomerDTO customerDto);
        Task<AuthResult> LoginAsync(LoginDTO loginDto);
    }
}
