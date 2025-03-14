using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Validation.AspNetCore;

namespace MyBudget.Api.Installers;

public sealed class AuthorizationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.AddAuthorization();
    }

    public static void Use(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}