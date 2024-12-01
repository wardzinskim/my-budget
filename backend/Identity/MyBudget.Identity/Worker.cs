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
        await CreateScopesAsync(scope);

        var client = await manager.FindByClientIdAsync("MyBudget.Frontend", cancellationToken);

        //if (client is not null)
        //{
        //    await manager.DeleteAsync(client);
        //    client = null;
        //}

        if (client is null)
        {
            await manager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "MyBudget.Frontend",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "MyBudget Frontend Application",
                    ClientType = ClientTypes.Public,
                    ApplicationType = ApplicationTypes.Web,
                    PostLogoutRedirectUris = {new Uri("http://localhost:5173")},
                    RedirectUris = {new Uri("http://localhost:5173")},
                    Permissions =
                    {
                        Permissions.Endpoints.EndSession,
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "my_budget.api"
                    },
                    Requirements = {Requirements.Features.ProofKeyForCodeExchange}
                }, cancellationToken);
        }


        client = await manager.FindByClientIdAsync("MyBudget.Backend", cancellationToken);

        //if (client is not null)
        //{
        //    await manager.DeleteAsync(client);
        //    client = null;
        //}

        if (client is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "MyBudget.Backend",
                ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                DisplayName = "MyBudget Backend Application",
                ApplicationType = ApplicationTypes.Web,
                Permissions = {Permissions.Endpoints.Introspection},
            }, cancellationToken);
        }
    }


    private async Task CreateScopesAsync(AsyncServiceScope scope)
    {
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        if (await manager.FindByNameAsync("my_budget.api") is null)
        {
            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = "my_budget.api", Resources = {"MyBudget.Backend"}
            });
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}