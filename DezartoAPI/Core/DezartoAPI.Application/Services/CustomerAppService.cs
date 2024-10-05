using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerAppService(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(ObjectId id)
        {
            var customer = await _customerService.GetByCustomerIdAsync(id);

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomerAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task AddCustomerAsync(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerService.AddCustomerAsync(customer);

            // DTO'da ID'yi geri döndürmek isterseniz
            customerDto.Id = customer.Id;
        }

        public async Task UpdateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerService.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(ObjectId id)
        {
            await _customerService.DeleteCustomerAsync(id);
        }

        public async Task<bool> CheckIfCustomerExistsAsync(string email)
        {
            return await _customerService.CheckIfCustomerExistsAsync(email);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _customerService.GetByEmailAsync(email);
        }
    }
}
