using AutoMapper;
using Pustok.Core.Entities;

namespace Pustok.Business.Profiles;

internal class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
    }
}
