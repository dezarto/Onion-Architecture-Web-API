using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Driver;

namespace DezartoAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _product;
        public ProductRepository(IMongoDatabase database)
        {
            _product = database.GetCollection<Product>("Products");
        }

        public async Task AddAsync(Product product)
        {
            await _product.InsertOneAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            await _product.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _product.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _product.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await _product.ReplaceOneAsync(c => c.Id == product.Id, product);
        }
    }
}
