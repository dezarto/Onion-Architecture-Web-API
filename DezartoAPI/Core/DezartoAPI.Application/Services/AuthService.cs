using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Domain.Entities;
using MongoDB.Bson;

namespace DezartoAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService; // Token üretimi için servis
        private readonly IPasswordHasher _passwordHasher; // Şifreyi hashlemek için servis

        public AuthService(ICustomerRepository customerRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> RegisterAsync(CustomerDTO customerDto)
        {
            if (await _customerRepository.CheckIfUserExistsAsync(customerDto.Email))
            {
                return new AuthResult { Success = false, Errors = new[] { "Email already in use." } };
            }

            var hashedPassword = _passwordHasher.HashPassword(customerDto.PasswordHash);

            var customer = new Customer
            {
                Name = customerDto.Name,
                Surname = customerDto.Surname,
                Email = customerDto.Email,
                Gender = customerDto.Gender,
                DateOfBirth = customerDto.DateOfBirth,
                PasswordHash = hashedPassword,
                PhoneNumber = customerDto.PhoneNumber,
                Addresses = customerDto.Addresses.Select(a => new Address
                {
                    NameOfAddress = a.NameOfAddress,
                    Country = a.Country,
                    City = a.City,
                    District = a.District,
                    Neighborhood = a.Neighborhood,
                    Street = a.Street,
                    PostalCode = a.PostalCode
                }).ToList(),  // Adresleri listeye çeviriyoruz
                UpdatedDate = DateTime.UtcNow,
                IsActive = true,
                Role = customerDto.Role,
                LoyaltyPoints = customerDto.LoyaltyPoints,
                OrderIds = customerDto.OrderIds ?? new List<ObjectId>()
            };

            await _customerRepository.AddAsync(customer);

            var token = _tokenService.GenerateToken(customer);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new AuthResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthResult> LoginAsync(LoginDTO loginDto)
        {
            var user = await _customerRepository.GetByEmailAsync(loginDto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return new AuthResult { Success = false, Errors = new[] { "Invalid email or password." } };
            }

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new AuthResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
