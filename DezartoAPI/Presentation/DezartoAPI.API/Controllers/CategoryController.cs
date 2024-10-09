using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryAppService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(ObjectId id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            await _categoryService.AddCategoryAsync(categoryDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, CategoryDTO categoryDto)
        {
            categoryDto.Id = id;
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
