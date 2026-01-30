using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pustok.Business.Dtos.TokenDtos;
using Pustok.Business.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pustok.Business.Services.Implementations;

internal class JWTService : IJWTService
{
    private readonly JWTOptionsDto _optionsDto;


    //options pattern
    public JWTService(IConfiguration configuration)
    {
        _optionsDto = configuration.GetSection("JWTOptions").Get<JWTOptionsDto>() ?? new();
    }

    public AccessTokenDto CreateAccessToken(List<Claim> claims)
    {
        JwtHeader jwtHeader = CreateJWTHeader();
        JwtPayload payload = CreateJWTPayload(claims);


        JwtSecurityToken jwtSecurityToken = new(jwtHeader, payload);

        JwtSecurityTokenHandler handler = new();


        string token = handler.WriteToken(jwtSecurityToken);


        return new()
        {
            Token = token,
            ExpiredDate = DateTime.UtcNow.AddMinutes(_optionsDto.ExpiredDate)
        };
    }

    private JwtPayload CreateJWTPayload(List<Claim> claims)
    {
        //Payload

        return new(
            issuer: _optionsDto.Issuer,
            audience: _optionsDto.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_optionsDto.ExpiredDate)
            );
    }

    private JwtHeader CreateJWTHeader()
    {
        //Header

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_optionsDto.SecretKey));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        JwtHeader jwtHeader = new(signingCredentials);
        return jwtHeader;
    }
}
