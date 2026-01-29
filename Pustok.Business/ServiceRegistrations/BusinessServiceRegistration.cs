using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.Services.Implementations;
using Pustok.Business.Validators.ProductValidators;

namespace Pustok.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {

        services.AddFluentValidationAutoValidation();


        services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

        AddServices(services);

        services.AddAutoMapper(typeof(BusinessServiceRegistration).Assembly);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}
