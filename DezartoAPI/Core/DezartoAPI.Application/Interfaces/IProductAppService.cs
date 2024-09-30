using DezartoAPI.Application.DTOs;
using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductDTO> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(string id);
    }
}
