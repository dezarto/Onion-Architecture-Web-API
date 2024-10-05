using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DezartoAPI.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _order;

        public OrderRepository(IMongoDatabase database)
        {
            _order = database.GetCollection<Order>("Orders");
        }

        public async Task AddAsync(Order order)
        {
            await _order.InsertOneAsync(order);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _order.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _order.Find(_ => true).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(ObjectId id)
        {
            return await _order.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _order.ReplaceOneAsync(c => c.Id == order.Id, order);
        }
    }
}
