using DezartoAPI.Domain.Entities;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(string id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string id);
    }
}
