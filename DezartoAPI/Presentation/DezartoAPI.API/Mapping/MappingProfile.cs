using AutoMapper;
using DezartoAPI.Domain.Entities;
using DezartoAPI.Application.DTOs;

namespace DezartoAPI.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<ProductInOrder, ProductInOrderDTO>().ReverseMap();

            // CartItemDTO -> ProductInOrder eşlemesi
            CreateMap<CartItemDTO, ProductInOrder>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            // CartDTO ve Cart eşlemesi
            CreateMap<CartDTO, Cart>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CartItemDTO, CartItem>().ReverseMap();

            CreateMap<CartDTO, OrderDTO>()
           .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
           .ForMember(dest => dest.OrderDate, opt => opt.Ignore())  // OrderDate manuel olarak set edilebilir.
           .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));  // Eşlemesi yapılacak

            // CartItemDTO -> ProductInOrder Map'leme
            CreateMap<CartItemDTO, ProductInOrder>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        }
    }
}
