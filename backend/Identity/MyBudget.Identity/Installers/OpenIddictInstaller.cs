using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstractions.Installer;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyBudget.Identity.Installers;

public class OpenIddictInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options
                    .UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddServer(options =>
            {
                options
                    .SetAuthorizationEndpointUris("authorize")
                    .SetTokenEndpointUris("token")
                    .SetLogoutEndpointUris("logout")
                    .SetUserinfoEndpointUris("userinfo")
                    .SetIntrospectionEndpointUris("introspect");

                options
                    .AllowAuthorizationCodeFlow();

                options
                    .RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "MyBudget");

                options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .DisableTransportSecurityRequirement()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }
}