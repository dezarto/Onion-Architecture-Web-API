﻿using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using MongoDB.Bson;

namespace DezartoAPI.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetByCustomerIdAsync(ObjectId id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }
        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _customerRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(ObjectId id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> CheckIfCustomerExistsAsync(string email)
        {
            return await _customerRepository.CheckIfUserExistsAsync(email);
        }
    }
}
