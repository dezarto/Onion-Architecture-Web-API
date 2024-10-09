using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Domain.Services;
using MongoDB.Bson;

namespace DezartoAPI.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryAppService(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryService.AddCategoryAsync(category);

            categoryDto.Id = category.Id;
        }

        public async Task DeleteCategoryAsync(ObjectId id)
        {
            await _categoryService.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(ObjectId id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryService.UpdateCategoryAsync(category);
        }
    }
}
