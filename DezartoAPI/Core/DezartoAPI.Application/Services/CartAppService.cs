using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Application.Services
{
    public class CartAppService : ICartAppService
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartAppService(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task AddCartAsync(CartDTO cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartService.AddCartAsync(cart);

            // DTO'da ID'yi geri döndürmek isterseniz
            cartDto.Id = cart.Id;
        }

        public async Task DeleteCartAsync(ObjectId id)
        {
            await _cartService.DeleteCartAsync(id);
        }

        public async Task<IEnumerable<CartDTO>> GetAllCartAsync()
        {
            var carts = await _cartService.GetAllCartAsync();
            return _mapper.Map<IEnumerable<CartDTO>>(carts);
        }

        public async Task<CartDTO> GetCartByIdAsync(ObjectId id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            return _mapper.Map<CartDTO>(cart);
        }

        public async Task UpdateCartAsync(CartDTO cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartService.UpdateCartAsync(cart);
        }

        public async Task<bool> CheckIfCartExistsAsync(ObjectId id)
        {
            return await _cartService.CheckIfCartExistsAsync(id);
        }
    }
}
