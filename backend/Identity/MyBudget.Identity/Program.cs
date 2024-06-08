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

        app.UseForwardedHeaders();

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