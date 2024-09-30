using DezartoAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezartoAPI.Application.Interfaces
{
    public interface ICustomerAppService
    {
        Task<CustomerDTO> GetCustomerByIdAsync(string id);
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerDTO customerDto);
        Task UpdateCustomerAsync(CustomerDTO customerDto);
        Task DeleteCustomerAsync(string id);
    }
}
