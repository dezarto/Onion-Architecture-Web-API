using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderAppService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(ObjectId id)
        {
            var product = await _orderService.GetOrderByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _orderService.GetAllOrderAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            await _orderService.AddOrderAsync(orderDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, OrderDTO orderDTO)
        {
            orderDTO.Id = id;
            await _orderService.UpdateOrderAsync(orderDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
