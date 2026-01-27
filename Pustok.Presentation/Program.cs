
using Pustok.Business.ServiceRegistrations;
using Pustok.DataAccess.ServiceRegistrations;

namespace Pustok.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        //builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddBusinessServices() //=>builder.Services.AddDataAccessServices(builder.Configuration);
            .AddDataAccessServices(builder.Configuration);

        //builder.Services.AddDataAccessServices(builder.Configuration);
        //builder.Services.AddBusinessServices();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Enables middleware to serve generated Swagger as a JSON endpoint
            app.UseSwaggerUI(); // Enables middleware to serve swagger-ui (HTML, JS, CSS, etc.)
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
