using Carter;
using MyBudget.Api.Installers.Abstraction;
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
        builder.Services.AddAuthorization();

        builder.Install(typeof(Program).Assembly);

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapCarter();
        app.MapHealthChecks("/health");
        app.Run();
    }
}