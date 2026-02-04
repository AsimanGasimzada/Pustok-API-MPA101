using System.Net;

namespace Pustok.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // required to read cookies from current HTTP context inside the handler
        builder.Services.AddHttpContextAccessor();



        // register the delegating handler that will attach Authorization header from cookie
        builder.Services.AddTransient<Pustok.MVC.Handlers.AuthTokenHandler>();

        // configure a named HttpClient for the API and attach the handler
        builder.Services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIUrl") ?? "");
        })
        .AddHttpMessageHandler<Pustok.MVC.Handlers.AuthTokenHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
