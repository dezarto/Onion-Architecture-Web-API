using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(ObjectId id);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(ObjectId id);
    }
}
