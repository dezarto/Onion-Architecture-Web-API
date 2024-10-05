using DezartoAPI.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(ObjectId id);
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(ObjectId id);
    }
}
