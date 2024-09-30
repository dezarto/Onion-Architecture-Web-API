using DezartoAPI.Application.DTOs;
using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<OrderDTO> GetOrderByIdAsync(string id);
        Task<IEnumerable<OrderDTO>> GetAllOrderAsync();
        Task AddOrderAsync(OrderDTO orderDto);
        Task UpdateOrderAsync(OrderDTO orderDto);
        Task DeleteOrderAsync(string id);
    }
}
