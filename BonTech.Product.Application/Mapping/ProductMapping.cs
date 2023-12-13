using AutoMapper;
using BonTech.Product.Domain.Dto;

namespace BonTech.Product.Application.Mapping;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Domain.Entity.Product, ProductDto>().ReverseMap();
        CreateMap<Domain.Entity.Product, OrderProductDto>().ReverseMap();
    }
}