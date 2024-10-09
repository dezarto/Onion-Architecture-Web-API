using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(ObjectId id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(ObjectId id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
