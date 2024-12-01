using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Identity.Installers;

public sealed class HealthChecksInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var healthCheckBuilder = services.AddHealthChecks();

        healthCheckBuilder.AddNpgSql(configuration.GetConnectionString("Default")!);
    }

    public void Use(WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}