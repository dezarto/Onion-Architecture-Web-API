using DezartoAPI.Application.Interfaces;
using DezartoAPI.Application.Services;
using DezartoAPI.API.Mapping;
using DezartoAPI.Domain.Interfaces;
using DezartoAPI.Infrastructure.Persistence;
using DezartoAPI.Infrastructure.Repositories;
using DezartoAPI.Domain.Services;
using MongoDB.Driver;
using DezartoAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS ayarları
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin() // Tüm kaynaklara izin ver
            .AllowAnyMethod() // Tüm HTTP yöntemlerine izin ver
            .AllowAnyHeader()); // Tüm başlıklara izin ver
});

// MongoDbContext ve IMongoDatabase'i kaydediyoruz
builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext("mongodb+srv://semir:semir222@dezarto.apohv9q.mongodb.net/?retryWrites=true&w=majority&appName=dezarto", "dezartoDB"));

builder.Services.AddSingleton<IMongoDatabase>(sp => sp.GetRequiredService<MongoDbContext>().Database);

// Dependency Injection for Repository and Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartAppService, CartAppService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// **JWT Authentication configuration**
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"]; // appsettings.json'dan JWT anahtarını al

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Token geçerlilik süresi kontrolünde esneklik
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS'u kullan
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **JWT Authentication Middleware**
app.UseAuthentication(); // <-- Authentication burada çağrılmalı
app.UseAuthorization();   // <-- Authorization ise sonrasında gelir

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
