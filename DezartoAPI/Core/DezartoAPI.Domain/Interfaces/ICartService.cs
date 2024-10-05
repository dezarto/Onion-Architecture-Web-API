using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartByIdAsync(ObjectId id);
        Task<IEnumerable<Cart>> GetAllCartAsync();
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(ObjectId id);
    }
}
