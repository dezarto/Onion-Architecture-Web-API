using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Domain.Entities;

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

        public async Task<AuthResult> RegisterAsync(RegisterDTO registerDto)
        {
            if (await _customerRepository.CheckIfUserExistsAsync(registerDto.Email))
            {
                return new AuthResult { Success = false, Errors = new[] { "Email already in use." } };
            }

            var hashedPassword = _passwordHasher.HashPassword(registerDto.Password);

            var customer = new Customer
            {
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                DateOfBirth = registerDto.DateOfBirth,
                Address = registerDto.Address,
                City = registerDto.City,
                Country = registerDto.Country,
                PostalCode = registerDto.PostalCode,
                Gender = registerDto.Gender,
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
