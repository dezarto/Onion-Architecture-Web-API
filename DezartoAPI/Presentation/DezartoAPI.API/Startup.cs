using DezartoAPI.Application.Interfaces;
using DezartoAPI.Application.Services;
using AutoMapper;
using DezartoAPI.API.Mapping;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Infrastructure.Persistence;
using DezartoAPI.Infrastructure.Repositories;
using DezartoAPI.Domain.Services;
using MongoDB.Driver;
using DezartoAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MongoDbContext ve IMongoDatabase'i kaydediyoruz
builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext("mongodb+srv://semir:semir222@dezarto.apohv9q.mongodb.net/?retryWrites=true&w=majority&appName=dezarto", "dezartoDB"));

builder.Services.AddSingleton<IMongoDatabase>(sp => sp.GetRequiredService<MongoDbContext>().Database);

// Dependency Injection for Repository and Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); // Customer Repository
builder.Services.AddScoped<ICustomerService, CustomerService>(); // Customer Service
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>(); // Customer Application Service

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AuthService ve gerekli bağımlılıklarını kaydedin
builder.Services.AddScoped<IAuthService, AuthService>(); // AuthService
builder.Services.AddScoped<ITokenService, TokenService>(); // TokenService
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>(); // PasswordHasher

// Product Service için Dependency Injection
builder.Services.AddScoped<IProductService, ProductService>(); // Product Domain Service
builder.Services.AddScoped<IProductAppService, ProductAppService>(); // Product Application Service
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Product Repository

//Order Service için Dependency Injection
builder.Services.AddScoped<IOrderService, OrderService>(); // Order Domain Service
builder.Services.AddScoped<IOrderAppService, OrderAppService>(); // Order Application Service
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); // Order Repository

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
