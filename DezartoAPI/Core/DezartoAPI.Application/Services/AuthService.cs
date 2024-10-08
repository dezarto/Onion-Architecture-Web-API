using DezartoAPI.Application.DTOs;
using DezartoAPI.Application.Interfaces;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Domain.Entities;
using MongoDB.Bson;
using AutoMapper;

namespace DezartoAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService; // Token üretimi için servis
        private readonly IPasswordHasher _passwordHasher; // Şifreyi hashlemek için servis
        private readonly IMapper _mapper;

        public AuthService(ICustomerRepository customerRepository, ITokenService tokenService, IPasswordHasher passwordHasher, ICustomerAppService customerAppService, IMapper mapper)
        {
            _customerAppService = customerAppService;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<AuthResult> RegisterAsync(RegisterDTO registerDto)
        {
            if (await _customerAppService.CheckIfCustomerExistsAsync(registerDto.Email))
            {
                return new AuthResult { Success = false, Errors = new[] { "Email already in use." } };
            }

            var hashedPassword = _passwordHasher.HashPassword(registerDto.PasswordHash);

            var customerDto = new CustomerDTO
            {
                Id = registerDto.Id,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                Gender = registerDto.Gender,
                DateOfBirth = registerDto.DateOfBirth,
                PasswordHash = hashedPassword,
                PhoneNumber = registerDto.PhoneNumber,
                UpdatedDate = DateTime.UtcNow,
                IsActive = true,
                Roles = registerDto.Roles,
                LoyaltyPoints = registerDto.LoyaltyPoints,
                CartId = registerDto.CartId,
            };

            await _customerAppService.AddCustomerAsync(customerDto);

            var token = _tokenService.GenerateToken(customerDto);
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
            var user = await _customerAppService.GetByCustomerEmailAsync(loginDto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return new AuthResult { Success = false, Errors = new[] { "Invalid email or password." } };
            }

            var userDto = _mapper.Map<CustomerDTO>(user);

            var token = _tokenService.GenerateToken(userDto);
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
