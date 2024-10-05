using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(ObjectId id);
    }
}
