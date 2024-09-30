using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderAppService(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderService.AddOrderAsync(order);

            // DTO'da ID'yi geri döndürmek isterseniz
            orderDto.Id = order.Id;
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orderService.DeleteOrderAsync(id);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrderAsync()
        {
            var orders = await _orderService.GetAllOrderAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task UpdateOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderService.UpdateOrderAsync(order);
        }
    }
}
