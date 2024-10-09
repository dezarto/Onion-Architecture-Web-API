using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DezartoAPI.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task AddAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _categories.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(ObjectId id)
        {
            return await _categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);
        }
    }
}
