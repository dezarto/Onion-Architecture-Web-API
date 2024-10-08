using DezartoAPI.Application.DTOs;
using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Application.Interfaces
{
    public interface ICustomerAppService
    {
        Task<CustomerDTO> GetCustomerByIdAsync(ObjectId id);
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerDTO customerDto);
        Task UpdateCustomerAsync(CustomerDTO customerDto);
        Task DeleteCustomerAsync(ObjectId id);
        Task<bool> CheckIfCustomerExistsAsync(string email);
        Task<CustomerDTO> GetByCustomerEmailAsync(string email);
    }
}
