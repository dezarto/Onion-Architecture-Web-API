using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductAppService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) // Guid yerine string kullanıyoruz
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            await _productService.AddProductAsync(productDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductDTO productDto)
        {
            productDto.Id = id;
            await _productService.UpdateProductAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
