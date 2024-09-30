using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartAppService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartAppService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _cartService.GetCartByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
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
        public async Task<IActionResult> Update(string id, CartDTO cartDTO)
        {
            cartDTO.Id = id;
            await _cartService.UpdateCartAsync(cartDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cartService.DeleteCartAsync(id);
            return NoContent();
        }
    }
}
