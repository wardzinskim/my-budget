
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Api.Installers;

public sealed class AuthorizationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddAuthorization();
    }

    public void Use(WebApplication app)
    {
        app.UseAuthorization();
    }

}