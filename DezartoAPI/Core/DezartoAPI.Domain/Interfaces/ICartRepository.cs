using DezartoAPI.Domain.Entities;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(string id);
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart> GetByIdAsync(string id);
    }
}
