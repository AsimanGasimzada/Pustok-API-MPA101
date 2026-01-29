
using Pustok.Business.ServiceRegistrations;
using Pustok.DataAccess.ServiceRegistrations;
using Pustok.Presentation.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Pustok.DataAccess.Abstractions;

namespace Pustok.Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();


        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            //builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
                   .AllowAnyHeader();
        }));


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        //builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddBusinessServices() //=>builder.Services.AddDataAccessServices(builder.Configuration);
            .AddDataAccessServices(builder.Configuration);

        //builder.Services.AddDataAccessServices(builder.Configuration);
        //builder.Services.AddBusinessServices();



        var app = builder.Build();


        var scope = app.Services.CreateScope();
        var initalizer = scope.ServiceProvider.GetRequiredService<IContextInitalizer>();
        await initalizer.InitDatabaseAsync();


        app.UseMiddleware<GlobalExceptionHandler>();

        app.UseCors("MyPolicy");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        await app.RunAsync();
    }
}
