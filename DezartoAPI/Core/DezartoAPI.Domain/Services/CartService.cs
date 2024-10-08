using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

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

        public async Task DeleteCartAsync(ObjectId id)
        {
            await _cartRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetAllCartAsync()
        {
            return await _cartRepository.GetAllAsync();
        }

        public async Task<Cart> GetCartByIdAsync(ObjectId id)
        {
            return await _cartRepository.GetByIdAsync(id);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task<bool> CheckIfCartExistsAsync(ObjectId id)
        {
            return await _cartRepository.CheckIfCartExistsAsync(id);
        }
    }
}
