using DezartoAPI.Domain.Entities;
using System.Linq.Expressions;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> GetByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(string id);
        Task<bool> CheckIfUserExistsAsync(string email);
    }
}
