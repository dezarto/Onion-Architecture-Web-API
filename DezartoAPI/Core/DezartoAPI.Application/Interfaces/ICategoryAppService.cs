using DezartoAPI.Application.DTOs;
using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(ObjectId id);
        Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync();
        Task AddCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(CategoryDTO categoryDto);
        Task DeleteCategoryAsync(ObjectId id);
    }
}
