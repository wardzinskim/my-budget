using MyBudget.Api.Installers.Abstraction;

namespace MyBudget.Api.Installers;

public sealed class HealthChecksInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var healthCheckBuilder = services.AddHealthChecks();
    }
}