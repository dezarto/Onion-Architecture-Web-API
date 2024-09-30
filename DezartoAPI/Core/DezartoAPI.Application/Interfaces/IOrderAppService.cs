using DezartoAPI.Application.DTOs;

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
