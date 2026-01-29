
using Pustok.Core.Entities;

namespace Pustok.Business.Profiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
    }
}
