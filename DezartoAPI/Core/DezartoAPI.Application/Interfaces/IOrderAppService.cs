using DezartoAPI.Application.DTOs;
using MongoDB.Bson;

namespace DezartoAPI.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<OrderDTO> GetOrderByIdAsync(ObjectId id);
        Task<IEnumerable<OrderDTO>> GetAllOrderAsync();
        Task AddOrderAsync(OrderDTO orderDto);
        Task UpdateOrderAsync(OrderDTO orderDto);
        Task DeleteOrderAsync(ObjectId id);
    }
}
