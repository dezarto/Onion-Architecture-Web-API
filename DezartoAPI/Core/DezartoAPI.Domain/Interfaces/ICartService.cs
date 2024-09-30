using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartByIdAsync(string id);
        Task<IEnumerable<Cart>> GetAllCartAsync();
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(string id);
    }
}
