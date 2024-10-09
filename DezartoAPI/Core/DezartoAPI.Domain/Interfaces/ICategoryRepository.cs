using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(ObjectId id);
    }
}
