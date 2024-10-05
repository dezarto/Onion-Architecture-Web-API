using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(ObjectId id);
    }
}
