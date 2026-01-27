using AutoMapper;
using Pustok.Core.Entities;

namespace Pustok.Business.Profiles;

internal class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductGetDto>()
            //.ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.Name))
            .ReverseMap();


        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
    }
}
