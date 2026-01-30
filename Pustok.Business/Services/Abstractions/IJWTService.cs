using System.Security.Claims;

namespace Pustok.Business.Services.Abstractions;

public interface IJWTService
{
    AccessTokenDto CreateAccessToken(List<Claim> claims);
}
