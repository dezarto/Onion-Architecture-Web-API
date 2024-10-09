using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task AddProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productService.AddProductAsync(product);

            productDto.Id = product.Id;
        }

        public async Task DeleteProductAsync(ObjectId id)
        {
             await _productService.DeleteProductAsync(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var products = await _productService.GetAllProductAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(ObjectId id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateProductAsync(product);
        }
    }
}
