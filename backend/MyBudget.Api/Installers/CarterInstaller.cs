using Carter;
using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Api.Installers;

public sealed class CarterInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddCarter();
    }

    public static void Use(WebApplication app)
    {
        app.MapCarter();
    }
}