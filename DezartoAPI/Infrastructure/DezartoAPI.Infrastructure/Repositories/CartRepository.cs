using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _cart;

        public CartRepository(IMongoDatabase database)
        {
            _cart = database.GetCollection<Cart>("Cart");
        }

        public async Task AddAsync(Cart cart)
        {
            await _cart.InsertOneAsync(cart);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _cart.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _cart.Find(_ => true).ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(ObjectId id)
        {
            return await _cart.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            await _cart.ReplaceOneAsync(c => c.Id == cart.Id, cart);
        }
    }
}
