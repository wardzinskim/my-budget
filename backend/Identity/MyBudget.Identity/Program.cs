using MyBudget.Infrastructure.Abstraction.Installer;
using Serilog;

namespace MyBudget.Identity;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(builder.Configuration);
        });
        builder
           .Install(typeof(Program).Assembly);


        // Add services to the container.
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddCors();
        builder.Services.AddAuthorization();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Use(typeof(Program).Assembly);
        app.Run();
    }
}
