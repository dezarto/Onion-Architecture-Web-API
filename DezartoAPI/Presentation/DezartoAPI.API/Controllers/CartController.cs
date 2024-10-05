using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartAppService _cartService;
        private readonly IProductAppService _productAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IMapper _mapper;

        public CartController(ICartAppService cartService, IProductAppService productService, IMapper mapper)
        {
            _cartService = cartService;
            _productAppService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var product = await _cartService.GetCartByIdAsync(objectId);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            else
            {
                throw new ArgumentException("Invalid id format.", nameof(id));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _cartService.GetAllCartAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartDTO cartDTO)
        {
            await _cartService.AddCartAsync(cartDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, CartDTO cartDTO)
        {
            cartDTO.Id = id;
            await _cartService.UpdateCartAsync(cartDTO);
            return Ok(cartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            await _cartService.DeleteCartAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDTO cartDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Müşteriyi al
            var customer = await _customerAppService.GetCustomerByIdAsync(cartDto.CustomerId);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            // Customer'ın CartId'sini al
            var cartId = customer.CartId == ObjectId.Empty ? ObjectId.GenerateNewId() : customer.CartId;

            var cart = await _cartService.GetCartByIdAsync(cartId);

            if (cart == null)
            {
                cart = new CartDTO
                {
                    Id = cartId,
                    CustomerId = cartDto.CustomerId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Items = new List<CartItem>() // CartItem tipi kullanılacak
                };
            }

            // cartDto.Items null veya boş olabilir, kontrol ekleyin
            if (cartDto.Items == null || cartDto.Items.Count == 0)
            {
                return BadRequest("No items in cart.");
            }

            // DTO'daki item'ları CartItem'a dönüştür
            var cartItems = _mapper.Map<List<CartItem>>(cartDto.Items);

            // Ürünleri kontrol edip cart'a ekleyin
            foreach (var cartItem in cartItems)
            {
                var product = await _productAppService.GetProductByIdAsync(cartItem.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {cartItem.ProductId} not found.");
                }

                decimal unitPrice = product.Price;
                cartItem.UnitPrice = unitPrice; // UnitPrice'ı ayarlayın

                // cart.Items.Add(cartItem);  // TotalPrice hesaplanır, bu satırı ekleyin
                cart.Items.Add(cartItem);  // Bu satırda hata yok, TotalPrice bir hesaplama.
            }

            cart.UpdatedDate = DateTime.UtcNow;

            await _cartService.AddCartAsync(cart);

            return Ok(cart);
        }

    }
}
