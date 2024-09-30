using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;

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

        public async Task<CustomerDTO> GetCustomerByIdAsync(string id)
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

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
        }
    }
}
