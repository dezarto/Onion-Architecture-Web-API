using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string id);
    }
}
