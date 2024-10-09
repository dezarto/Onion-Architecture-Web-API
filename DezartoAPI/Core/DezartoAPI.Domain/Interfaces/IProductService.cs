using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(ObjectId id);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(ObjectId id);
    }
}
