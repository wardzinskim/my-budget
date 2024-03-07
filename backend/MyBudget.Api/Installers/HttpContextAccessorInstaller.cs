using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Api.Installers;

public sealed class HttpContextAccessorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddHttpContextAccessor();
    }
}