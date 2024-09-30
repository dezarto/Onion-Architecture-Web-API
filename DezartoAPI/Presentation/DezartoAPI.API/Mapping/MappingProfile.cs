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
        }
    }
}
