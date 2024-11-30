using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Abstractions;
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
                    .SetEndSessionEndpointUris("endsession")
                    .SetUserInfoEndpointUris("userinfo")
                    .SetIntrospectionEndpointUris("introspect");

                options
                    .AllowAuthorizationCodeFlow();

                options
                    .RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "MyBudget");

                options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.RequireProofKeyForCodeExchange();

                options
                    .UseAspNetCore()
                    .DisableTransportSecurityRequirement()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableEndSessionEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .EnablePostLogoutRedirectionEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                    .EnableRedirectionEndpointPassthrough();

                options.UseSystemNetHttp()
                    .SetProductInformation(typeof(Program).Assembly);

                options.UseWebProviders()
                    .AddGoogle(option =>
                    {
                        option
                            .SetClientId(configuration["ExternalProviders:Google:ClientId"]!)
                            .SetClientSecret(configuration["ExternalProviders:Google:ClientSecret"]!)
                            .SetRedirectUri("callback/login/google")
                            .AddScopes(configuration["ExternalProviders:Google:Scopes"]!.Split(' '));
                    });
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }
}