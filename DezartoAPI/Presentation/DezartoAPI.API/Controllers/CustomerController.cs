using AutoMapper;
using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DezartoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerAppService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDto)
        {
            //await _customerService.CreateUserAsync(customerDto);
            //return CreatedAtAction(nameof(GetById), new { id = customerDto.Id }, customerDto);

            await _customerService.AddCustomerAsync(customerDto);

            // Burada createdCustomer.Id, MongoDB'nin oluşturduğu yeni ID olacak.
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CustomerDTO customerDto)
        {
            customerDto.Id = id;
            await _customerService.UpdateCustomerAsync(customerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
