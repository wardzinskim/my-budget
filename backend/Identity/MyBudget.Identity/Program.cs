using Microsoft.AspNetCore.HttpOverrides;
using MyBudget.Identity.Installers;
using MyBudget.Infrastructure.Abstractions.Installer;
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
        builder.Services.AddHostedService<Worker>();

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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        MvcInstaller.Use(app);
        HealthChecksInstaller.Use(app);
        GrpcInstaller.Use(app);

        app.Run();
    }
}