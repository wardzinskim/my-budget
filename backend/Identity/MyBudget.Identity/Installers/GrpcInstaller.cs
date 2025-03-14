using MyBudget.Identity.grpc.Services;
using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Identity.Installers;

public class GrpcInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddGrpc();
    }

    public static void Use(WebApplication app)
    {
        app.UseGrpcWeb(new GrpcWebOptions() {DefaultEnabled = true});

        app.MapGrpcService<UsersService>()
            .RequireAuthorization("MyBudgetIdentity");
    }
}