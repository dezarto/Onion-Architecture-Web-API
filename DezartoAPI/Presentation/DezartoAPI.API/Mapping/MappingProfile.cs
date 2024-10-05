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
            // Customer ve CustomerDTO eşlemesi
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.OrderIds, opt => opt.MapFrom(src => src.OrderIds))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

            // Address ve AddressDTO eşlemesi
            CreateMap<Address, AddressDTO>();

            CreateMap<CartDTO, Cart>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Cart, CartDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CartItemDTO, CartItem>().ReverseMap();

            CreateMap<CartItemDTO, CartItem>().ReverseMap();
        }
    }
}
