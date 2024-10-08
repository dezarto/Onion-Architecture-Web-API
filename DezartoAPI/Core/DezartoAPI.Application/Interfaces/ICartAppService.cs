using DezartoAPI.Application.DTOs;
using MongoDB.Bson;

namespace DezartoAPI.Application.Interfaces
{
    public interface ICartAppService
    {
        Task<CartDTO> GetCartByIdAsync(ObjectId id);
        Task<IEnumerable<CartDTO>> GetAllCartAsync();
        Task AddCartAsync(CartDTO cartDto);
        Task UpdateCartAsync(CartDTO cartDto);
        Task DeleteCartAsync(ObjectId id);
        Task<bool> CheckIfCartExistsAsync(ObjectId id);
    }
}
