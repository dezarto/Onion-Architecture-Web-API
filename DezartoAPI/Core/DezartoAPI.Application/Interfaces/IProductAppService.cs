using DezartoAPI.Application.DTOs;
using MongoDB.Bson;

namespace DezartoAPI.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductDTO> GetProductByIdAsync(ObjectId id);
        Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(ObjectId id);
    }
}
