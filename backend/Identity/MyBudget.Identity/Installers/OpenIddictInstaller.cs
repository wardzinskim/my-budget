using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Abstractions;
using OpenIddict.Client;
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

                options
                    .UseQuartz();
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
                    .AllowAuthorizationCodeFlow()
                    .AllowClientCredentialsFlow()
                    .AllowRefreshTokenFlow();

                options
                    .RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "my_budget.api", "my_budget.identity");

                options
                    .AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
                options.RequireProofKeyForCodeExchange();

                var oidcConfig = options
                    .UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableEndSessionEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();

                if (hostingEnvironment.IsDevelopment())
                {
                    oidcConfig
                        .DisableTransportSecurityRequirement();
                }
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


                // Ustaw AuthorizationResponseIssParameterSupported = true dla Google
                // ponieważ Google zwraca 'iss' w callbacku ale nie deklaruje tego w discovery
                options.AddEventHandler<OpenIddictClientEvents.HandleConfigurationResponseContext>(builder =>
                    builder.UseInlineHandler(context =>
                    {
                        if (context.Registration.ProviderName is
                            OpenIddict.Client.WebIntegration.OpenIddictClientWebIntegrationConstants.Providers.Google)
                        {
                            context.Configuration.AuthorizationResponseIssParameterSupported = true;
                        }

                        return ValueTask.CompletedTask;
                    }));
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.SetIssuer(configuration["OpenIddict:Issuer"]!);
                options.AddAudiences(configuration["OpenIddict:Audience"]!);
                options.UseAspNetCore();
            });
    }
}