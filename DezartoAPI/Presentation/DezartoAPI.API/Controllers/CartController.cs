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
        private readonly IOrderAppService _orderAppService;
        private readonly IMapper _mapper;

        public CartController(ICartAppService cartAppService, IProductAppService productService, IMapper mapper, ICustomerAppService customerAppService, IOrderAppService orderAppService)
        {
            _customerAppService = customerAppService;
            _cartAppService = cartAppService;
            _productAppService = productService;
            _mapper = mapper;
            _orderAppService = orderAppService;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _cartAppService.GetAllCartAsync();
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CartDTO cartDTO)
        {
            await _cartAppService.AddCartAsync(cartDTO);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, CartDTO cartDTO)
        {
            cartDTO.Id = id;
            await _cartAppService.UpdateCartAsync(cartDTO);
            return Ok(cartDTO);
        }

        [Authorize(Roles = "Admin")]
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
            }
            else
            {
                cart.UpdatedDate = DateTime.UtcNow;
            }

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

                    cartItem.UnitPrice = product.UnitPrice;
                    cartItem.TotalPrice = cartItem.Quantity * product.UnitPrice;

                    var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += cartItem.Quantity;
                        existingItem.TotalPrice = existingItem.Quantity * product.UnitPrice;
                    }
                    else
                    {
                        cart.Items.Add(new CartItemDTO
                        {
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            UnitPrice = cartItem.UnitPrice,
                            TotalPrice = cartItem.TotalPrice
                        });
                    }
                }
                else
                {
                    throw new ArgumentException("The product ID is not a valid ObjectId.", nameof(cartItem.ProductId));
                }
            }

            var totalPriceFromItems = cartDto.Items.Select(x => x.TotalPrice).Sum();
            cart.TotalPrice = cart.TotalPrice != 0 ? cart.TotalPrice + totalPriceFromItems : totalPriceFromItems;

            if (await _cartAppService.CheckIfCartExistsAsync(customer.CartId))
            {
                await _cartAppService.UpdateCartAsync(cart);
            }
            else
            {
                await _cartAppService.AddCartAsync(cart);
            }

            return Ok(cart);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveItemDTO removeItemDto)
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
                return NotFound("Cart not found.");
            }

            if (removeItemDto.ProductId == null)
            {
                return BadRequest("ProductId is required.");
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == removeItemDto.ProductId);

            if (cartItem != null)
            {
                if (cartItem.Quantity == 1)
                {
                    cart.Items.Remove(cartItem);

                    if (!cart.Items.Any())
                    {
                        await _cartAppService.DeleteCartAsync(cart.Id);
                        return Ok("Cart is empty and has been deleted.");
                    }
                }

                if (cartItem.Quantity > 1) 
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                    cartItem.TotalPrice = cartItem.Quantity * cartItem.UnitPrice;
                }

                var totalPriceFromItems = cart.Items.Select(x => x.TotalPrice).Sum();
                cart.TotalPrice = totalPriceFromItems;

                await _cartAppService.UpdateCartAsync(cart);
            }
            else
            {
                return NotFound("Product not found in cart.");
            }

            return Ok(cart);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutCart()
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

            if (cart == null || !cart.Items.Any())
            {
                return BadRequest("Cart is empty.");
            }

            // AutoMapper kullanarak cart'tan order'a dönüşüm
            var order = _mapper.Map<OrderDTO>(cart);
            order.CustomerId = customer.Id;
            order.OrderDate = DateTime.UtcNow;
            order.Id = ObjectId.GenerateNewId();

            await _orderAppService.AddOrderAsync(order);

            cart.Items.Clear();
            cart.TotalPrice = 0;
            cart.UpdatedDate = DateTime.UtcNow;
            await _cartAppService.UpdateCartAsync(cart);

            customer.OrderIds.Add(order.Id.ToString());

            await _customerAppService.UpdateCustomerAsync(customer);

            return Ok(order);
        }

    }
}
