using MyBudget.Infrastructure.Abstractions.Installer;
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

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });


        services.AddHttpClient(typeof(OpenIddictValidationSystemNetHttpOptions).Assembly.GetName().Name)
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });
    }
}