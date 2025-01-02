using Grpc.Net.Client.Web;
using MyBudget.Identity.Contract;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Client;

namespace MyBudget.Api.Installers;

public class GrpcClientInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddGrpcClient<Users.UsersClient>((_, options) =>
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
            })
            .ConfigureChannel(options =>
            {
                var httpClientHandler = new HttpClientHandler();
                if (hostingEnvironment.IsDevelopment())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                options.HttpHandler =
                    new GrpcWebHandler(GrpcWebMode.GrpcWebText, httpClientHandler); // gRPC-Web przez HTTP/1.1
            });
    }
}