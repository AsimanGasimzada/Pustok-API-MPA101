using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.Services.Implementations;

namespace Pustok.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        services.AddAutoMapper( typeof(BusinessServiceRegistration).Assembly);

        return services;
    }
}
