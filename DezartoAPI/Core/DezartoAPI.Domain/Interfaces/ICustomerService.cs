using DezartoAPI.Domain.Entities.Common;
using DezartoAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetByCustomerIdAsync(string id);
        Task<Customer> GetByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
    }
}