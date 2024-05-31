using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.Infrastructure.Installers;
using Serilog;

namespace MyBudget.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(builder.Configuration);
        });

        // Add services to the container.
        builder
            .Install(typeof(Program).Assembly)
            .Install(typeof(RepositoryInstaller).Assembly);

        var app = builder.Build();
        app.UsePathBase("/api");
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.Use(typeof(Program).Assembly)
           .Use(typeof(RepositoryInstaller).Assembly);

        app.Run();
    }
}