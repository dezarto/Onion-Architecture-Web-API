using DezartoAPI.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Customer> RegisterAsync(Customer customer, string password);
        Task<string> LoginAsync(string email, string password);
        Task<RefreshToken> GenerateRefreshTokenAsync();
    }
}