using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Authentication;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace MyBudget.Api.Installers;

public sealed class AuthorizationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services
            .AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
            .AddMcp(options =>
            {
                options.ForwardAuthenticate = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.ResourceMetadata = new ProtectedResourceMetadata()
                {
                    Resource = configuration["Resources:Mcp"]!,
                    AuthorizationServers =
                    [
                        new Uri(configuration["OpenIddict:Issuer"]!, UriKind.Absolute).ToString()
                    ],
                    ScopesSupported = ["my_budget.mcp"]
                };
            });
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy =
                new AuthorizationPolicyBuilder(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context =>
                        context.User.HasScope("my_budget.api")
                    )
                    .Build();
        });
    }

    public static void Use(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}