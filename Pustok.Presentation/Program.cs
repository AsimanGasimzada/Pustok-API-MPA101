
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Dtos;
using Pustok.Business.ServiceRegistrations;
using Pustok.DataAccess.Abstractions;
using Pustok.DataAccess.ServiceRegistrations;
using Pustok.Presentation.Middlewares;

namespace Pustok.Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => new
                    {
                        Errors = x.Value!.Errors.Select(e => e.ErrorMessage)
                    });

                ResultDto response = new ResultDto
                {
                    IsSucced = false,
                    Message = string.Join(", ", errors.SelectMany(x => x.Errors)),
                    StatusCode = 400
                };

                return new BadRequestObjectResult(response);
            };
        }); ;


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
        builder.Services.AddDataAccessServices(builder.Configuration);
        builder.Services.AddBusinessServices(builder.Configuration);//=>builder.Services.AddDataAccessServices(builder.Configuration);

        //builder.Services.AddDataAccessServices(builder.Configuration);
        //builder.Services.AddBusinessServices();



        var app = builder.Build();


        var scope = app.Services.CreateScope();
        var initalizer = scope.ServiceProvider.GetRequiredService<IContextInitalizer>();
        await initalizer.InitDatabaseAsync();


        app.UseMiddleware<GlobalExceptionHandler>();

        //if (!app.Environment.IsDevelopment())

        app.UseCors("MyPolicy");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        await app.RunAsync();
    }
}
