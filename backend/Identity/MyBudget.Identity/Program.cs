using Microsoft.AspNetCore.HttpOverrides;
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
        app.UsePathBase("/identity");

        var forwardedHeaderOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };
        forwardedHeaderOptions.KnownNetworks.Clear();
        forwardedHeaderOptions.KnownProxies.Clear();
        app.UseForwardedHeaders(forwardedHeaderOptions);

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
        app.Use(typeof(Program).Assembly);
        app.Run();
    }
}