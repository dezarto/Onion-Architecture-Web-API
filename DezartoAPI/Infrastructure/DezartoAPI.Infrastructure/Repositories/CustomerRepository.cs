using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DezartoAPI.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _customers.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customers.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customers.ReplaceOneAsync(c => c.Id == customer.Id, customer);
        }

        public async Task DeleteAsync(string id)
        {
            await _customers.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<bool> CheckIfUserExistsAsync(string email)
        {
            var customer = await GetByEmailAsync(email);
            return customer != null;
        }
    }
}
