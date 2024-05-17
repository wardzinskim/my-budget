
using MyBudget.Identity.Data;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MyBudget.Identity;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        //if (await manager.FindByClientIdAsync("MyBudget.Frontend") is null)
        //{
        //    await manager.CreateAsync(new OpenIddictApplicationDescriptor
        //    {
        //        ClientId = "MyBudget.Frontend",
        //        ConsentType = ConsentTypes.Explicit,
        //        DisplayName = "Blazor client application",
        //        ClientType = ClientTypes.Public,
        //        PostLogoutRedirectUris =
        //        {
        //            new Uri("https://localhost:44310/authentication/logout-callback")
        //        },
        //        RedirectUris =
        //        {
        //            new Uri("https://localhost:44310/authentication/login-callback")
        //        },
        //        Permissions =
        //        {
        //            Permissions.Endpoints.Authorization,
        //            Permissions.Endpoints.Logout,
        //            Permissions.Endpoints.Token,
        //            Permissions.GrantTypes.AuthorizationCode,
        //            Permissions.GrantTypes.RefreshToken,
        //            Permissions.ResponseTypes.Code,
        //            Permissions.Scopes.Email,
        //            Permissions.Scopes.Profile,
        //            Permissions.Scopes.Roles
        //        },
        //        Requirements =
        //        {
        //            Requirements.Features.ProofKeyForCodeExchange
        //        }
        //    });
        //}


        if (await manager.FindByClientIdAsync("oidc-debugger") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "oidc-debugger",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Oid Test",
                ClientType = ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("https://oidcdebugger.com/debug")
                },
                RedirectUris =
                {
                    new Uri("https://oauth.pstmn.io/v1/callback")
                },
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
                }
            });
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
     => Task.CompletedTask;
}
