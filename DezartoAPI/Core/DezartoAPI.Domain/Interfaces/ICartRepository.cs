using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart> GetByIdAsync(ObjectId id);
    }
}
