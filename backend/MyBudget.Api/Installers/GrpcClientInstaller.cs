using MyBudget.Identity.Contract;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Client;

namespace MyBudget.Api.Installers;

public class GrpcClientInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var grpcClientConfiguration = services.AddGrpcClient<Users.UsersClient>((_, options) =>
            {
                options.Address = new Uri(configuration["MyBudget.Identity:ApiUrl"]!);
            })
            .AddCallCredentials(async (_, metadata, serviceProvider) =>
            {
                var tokenService = serviceProvider.GetRequiredService<OpenIddictClientService>();
                var registration = await tokenService.GetClientRegistrationByProviderNameAsync("default");
                var result =
                    await tokenService.AuthenticateWithClientCredentialsAsync(new()
                    {
                        ProviderName = "default", Scopes = registration.Scopes.ToList()
                    });

                metadata.Add("Authorization", $"Bearer {result.AccessToken}");
            });

        if (hostingEnvironment.IsDevelopment())
        {
            grpcClientConfiguration.ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });
        }
    }
}