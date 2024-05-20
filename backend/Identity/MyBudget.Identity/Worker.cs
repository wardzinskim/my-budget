using MyBudget.Identity.Data;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyBudget.Identity;

public class Worker(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        var client = await manager.FindByClientIdAsync("MyBudget.Frontend");

        if (client is not null)
        {
            await manager.DeleteAsync(client);
        }

        await manager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "MyBudget.Frontend",
            ConsentType = ConsentTypes.Explicit,
            DisplayName = "MyBudget Frontend Application",
            ClientType = ClientTypes.Public,
            ApplicationType = ApplicationTypes.Web,
            PostLogoutRedirectUris = {new Uri("http://localhost:4000/authentication/logout-callback")},
            RedirectUris = {new Uri("http://localhost:5173")},
            Permissions =
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Logout,
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.AuthorizationCode,
                Permissions.GrantTypes.RefreshToken,
                Permissions.ResponseTypes.Code,
                Permissions.Scopes.Email,
                Permissions.Scopes.Profile,
                Permissions.Scopes.Roles
            },
            Requirements = {Requirements.Features.ProofKeyForCodeExchange}
        });
    }


    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}