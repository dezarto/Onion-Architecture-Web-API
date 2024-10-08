using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
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
        private readonly ICartAppService _cartAppService;
        private readonly IProductAppService _productAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IMapper _mapper;

        public CartController(ICartAppService cartAppService, IProductAppService productService, IMapper mapper, ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
            _cartAppService = cartAppService;
            _productAppService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var product = await _cartAppService.GetCartByIdAsync(objectId);
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
            var products = await _cartAppService.GetAllCartAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CartDTO cartDTO)
        {
            await _cartAppService.AddCartAsync(cartDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, CartDTO cartDTO)
        {
            cartDTO.Id = id;
            await _cartAppService.UpdateCartAsync(cartDTO);
            return Ok(cartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            await _cartAppService.DeleteCartAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDTO cartDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _customerAppService.GetByCustomerEmailAsync(userEmail);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            var cart = await _cartAppService.GetCartByIdAsync(customer.CartId);

            if (cart == null)
            {
                cart = new CartDTO
                {
                    Id = customer.CartId,
                    CustomerId = customer.Id,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Items = new List<CartItemDTO>()
                };

                if (cartDto.Items == null || cartDto.Items.Count == 0)
                {
                    return BadRequest("No items in cart.");
                }

                foreach (var cartItem in cartDto.Items)
                {
                    if (ObjectId.TryParse(cartItem.ProductId, out ObjectId productId))
                    {
                        var product = await _productAppService.GetProductByIdAsync(productId);

                        if (product == null)
                        {
                            return NotFound($"Product with ID {cartItem.ProductId} not found.");
                        }

                        decimal unitPrice = product.Price;
                        cartItem.UnitPrice = unitPrice;

                        cartItem.TotalPrice = cartItem.Quantity * unitPrice;

                        cart.Items.Add(new CartItemDTO
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            TotalPrice = cartItem.TotalPrice
                        });
                    }
                    else
                    {
                        throw new ArgumentException("The product ID is not a valid ObjectId.", nameof(cartItem.ProductId));
                    }
                }

                await _cartAppService.AddCartAsync(cart);
            }
            else
            {
                foreach (var cartItem in cartDto.Items)
                {
                    if (ObjectId.TryParse(cartItem.ProductId, out ObjectId productId))
                    {
                        var product = await _productAppService.GetProductByIdAsync(productId);

                        if (product == null)
                        {
                            return NotFound($"Product with ID {cartItem.ProductId} not found.");
                        }

                        decimal unitPrice = product.Price;
                        cartItem.UnitPrice = unitPrice;

                        cartItem.TotalPrice = cartItem.Quantity * unitPrice;

                        cart.Items.Add(new CartItemDTO
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            TotalPrice = cartItem.TotalPrice
                        });
                    }
                    else
                    {
                        throw new ArgumentException("The product ID is not a valid ObjectId.", nameof(cartItem.ProductId));
                    }
                }

                await _cartAppService.UpdateCartAsync(cart);
            }

            return Ok(cart);
        }
    }
}
