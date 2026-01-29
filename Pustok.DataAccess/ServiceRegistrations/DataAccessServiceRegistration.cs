using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pustok.DataAccess.Abstractions;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.DataInitalizers;
using Pustok.DataAccess.Interceptors;
using Pustok.DataAccess.Repositories.Abstractions;
using Pustok.DataAccess.Repositories.Implementations;

namespace Pustok.DataAccess.ServiceRegistrations;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContextInitalizer, DbContextInitalizer>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("Default"));
        });


        services.AddIdentity<AppUser, AppRole>(options =>
        {

            options.Password.RequiredLength = 5;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;

            options.User.RequireUniqueEmail = true;

            //options.SignIn.RequireConfirmedEmail = true;
        }).AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<BaseAuditableInterceptor>();

        return services;
    }
}
