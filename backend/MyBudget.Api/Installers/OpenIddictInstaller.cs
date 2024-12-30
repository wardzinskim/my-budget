using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Client;
using OpenIddict.Validation.SystemNetHttp;

namespace MyBudget.Api.Installers;

public class OpenIddictInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services
            .AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(configuration["OpenIddict:Issuer"]!);

                options.AddAudiences(configuration["OpenIddict:Audience"]!);

                options.UseIntrospection()
                    .SetClientId(configuration["OpenIddict:ClientId"]!)
                    .SetClientSecret(configuration["OpenIddict:ClientSecret"]!);

                options.UseSystemNetHttp().ConfigureHttpClientHandler(x =>
                    {
                        if (hostingEnvironment.IsDevelopment())
                        {
                            x.ServerCertificateCustomValidationCallback =
                                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                        }
                    }
                );
                options.UseAspNetCore();
            })
            .AddClient(options =>
            {
                options.AllowClientCredentialsFlow();
                options.UseSystemNetHttp()
                    .ConfigureHttpClientHandler(x =>
                    {
                        if (hostingEnvironment.IsDevelopment())
                        {
                            x.ServerCertificateCustomValidationCallback =
                                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                        }
                    });
                options.UseAspNetCore();

                var clientConfiguration = new OpenIddictClientRegistration()
                {
                    ClientId = configuration["OpenIddict:ClientId"]!,
                    ClientSecret = configuration["OpenIddict:ClientSecret"]!,
                    Issuer = new Uri(configuration["OpenIddict:Issuer"]!, UriKind.Absolute),
                    ProviderName = "default"
                };

                
                foreach (string scope in configuration["OpenIddict:Scopes"]!.Split(" "))
                {
                    clientConfiguration.Scopes.Add(scope);
                }

                options.AddRegistration(clientConfiguration);
            });
    }
}