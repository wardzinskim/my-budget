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
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        await CreateScopesAsync(scope);

        var client = await manager.FindByClientIdAsync("MyBudget.Frontend", cancellationToken);

        //if (client is not null)
        //{
        //    await manager.DeleteAsync(client, cancellationToken);
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
                    RedirectUris =
                    {
                        new Uri("http://localhost:5173"), new Uri("http://localhost:5173/silent-renew.html")
                    },
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
        //    await manager.DeleteAsync(client, cancellationToken);
        //   client = null;
        //}

        if (client is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "MyBudget.Backend",
                ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                DisplayName = "MyBudget Backend Application",
                ApplicationType = ApplicationTypes.Web,
                Permissions =
                {
                    Permissions.Endpoints.Introspection,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "my_budget.identity"
                },
            }, cancellationToken);
        }

        client = await manager.FindByClientIdAsync("MyBudget.Identity", cancellationToken);

        //if (client is not null)
        //{
        //    await manager.DeleteAsync(client, cancellationToken);
        //    client = null;
        //}

        if (client is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "MyBudget.Identity",
                ClientSecret = "57e9b95b-7e51-4917-bdde-34d5fa83dbfd",
                DisplayName = "MyBudget Identity Application",
                ApplicationType = ApplicationTypes.Web,
            }, cancellationToken);
        }

        client = await manager.FindByClientIdAsync("MyBudget.MCP", cancellationToken);

        //if (client is not null)
        //{
        //    await manager.DeleteAsync(client, cancellationToken);
        //    client = null;
        //}

        if (client is null)
        {
            await manager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "MyBudget.MCP",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "MyBudget MCP Client",
                    ClientType = ClientTypes.Public,
                    ApplicationType = ApplicationTypes.Web,
                    RedirectUris = {new Uri("http://127.0.0.1:6274/oauth/callback")},
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
                        Permissions.Prefixes.Scope + "my_budget.mcp",
                        Permissions.Prefixes.Resource + configuration["Resources:MCP"]
                    },
                    Requirements = {Requirements.Features.ProofKeyForCodeExchange}
                }, cancellationToken);
        }
    }


    private static async Task CreateScopesAsync(AsyncServiceScope scope)
    {
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        await UpsertScopeAsync(manager, "my_budget.api", "MyBudget.Backend");
        await UpsertScopeAsync(manager, "my_budget.mcp", "MyBudget.Backend");
        await UpsertScopeAsync(manager, "my_budget.identity", "MyBudget.Identity");
    }

    private static async Task UpsertScopeAsync(IOpenIddictScopeManager manager, string name, string resource)
    {
        var descriptor = new OpenIddictScopeDescriptor {Name = name};
        descriptor.Resources.Add(resource);

        var existing = await manager.FindByNameAsync(name);
        if (existing is null)
        {
            await manager.CreateAsync(descriptor);
        }
        else
        {
            await manager.UpdateAsync(existing, descriptor);
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}