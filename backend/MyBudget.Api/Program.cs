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
        builder.Install(typeof(Program).Assembly);

        var app = builder.Build();
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.Use(typeof(Program).Assembly);

        app.Run();
    }
}