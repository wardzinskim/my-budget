using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Api.Installers;

public sealed class HealthChecksInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var healthCheckBuilder = services.AddHealthChecks();
    }

    public void Use(WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}