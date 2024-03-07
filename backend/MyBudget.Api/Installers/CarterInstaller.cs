using Carter;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Api.Installers;

public sealed class CarterInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddCarter();
    }

    public void Use(WebApplication app)
    {
        app.MapCarter();
    }
}