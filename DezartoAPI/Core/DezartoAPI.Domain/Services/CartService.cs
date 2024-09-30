using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;

namespace DezartoAPI.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _cartRepository.AddAsync(cart);
        }

        public async Task DeleteCartAsync(string id)
        {
            await _cartRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetAllCartAsync()
        {
            return await _cartRepository.GetAllAsync();
        }

        public async Task<Cart> GetCartByIdAsync(string id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await _cartRepository.UpdateAsync(cart);
        }
    }
}
