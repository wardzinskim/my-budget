using Microsoft.AspNetCore.HttpOverrides;
using MyBudget.Api.Installers;
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

        var basePath = app.Configuration["BasePath"];
        if (!string.IsNullOrWhiteSpace(basePath))
        {
            app.UsePathBase(basePath);
        }

        var forwardingOptions = new ForwardedHeadersOptions()
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        forwardingOptions.KnownNetworks.Clear(); // Loopback by default, this should be temporary
        forwardingOptions.KnownProxies.Clear(); // Update to include
        app.UseForwardedHeaders(forwardingOptions);

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        
        CorsInstaller.Use(app);
        AuthorizationInstaller.Use(app);
        HealthChecksInstaller.Use(app);
        SwaggerInstaller.Use(app);
        CarterInstaller.Use(app);
        
        app.Run();
    }
}