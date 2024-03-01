using Carter;
using MyBudget.Api.Installers.Abstraction;

namespace MyBudget.Api.Installers;

public sealed class CarterInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddCarter();
    }
}