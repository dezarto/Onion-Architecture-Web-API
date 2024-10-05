using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetByCustomerIdAsync(ObjectId id);
        Task<Customer> GetByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(ObjectId id);
        Task<bool> CheckIfCustomerExistsAsync(string email);
    }
}