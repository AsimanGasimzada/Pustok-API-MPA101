using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pustok.Business.Dtos.TokenDtos;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Validators.ProductValidators;
using System.Text;

namespace Pustok.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{

    public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddFluentValidationAutoValidation();


        services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

        AddServices(services);

        services.AddAutoMapper(typeof(BusinessServiceRegistration).Assembly);



        var jwtOptionsDto = configuration.GetSection("JWTOptions").Get<JWTOptionsDto>() ?? new();

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtOptionsDto.Issuer,
                ValidAudience = jwtOptionsDto.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsDto.SecretKey)),
                RoleClaimType = "Role"
            };
        });

        //Authorization:Bearer {token}

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJWTService, JWTService>();

    }
}
