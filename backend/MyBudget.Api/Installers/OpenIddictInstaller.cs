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

                options.AddAudiences("MyBudget.Backend");

                options.UseIntrospection()
                    .SetClientId("MyBudget.Backend")
                    .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");


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