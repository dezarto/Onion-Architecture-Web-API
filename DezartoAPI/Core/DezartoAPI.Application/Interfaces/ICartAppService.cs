using DezartoAPI.Application.DTOs;

namespace DezartoAPI.Application.Interfaces
{
    public interface ICartAppService
    {
        Task<CartDTO> GetCartByIdAsync(string id);
        Task<IEnumerable<CartDTO>> GetAllCartAsync();
        Task AddCartAsync(CartDTO cartDto);
        Task UpdateCartAsync(CartDTO cartDto);
        Task DeleteCartAsync(string id);
    }
}
