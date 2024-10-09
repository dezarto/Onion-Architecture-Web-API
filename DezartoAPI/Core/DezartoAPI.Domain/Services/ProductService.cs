using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(ObjectId id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(ObjectId id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
