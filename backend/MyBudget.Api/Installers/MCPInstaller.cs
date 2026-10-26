using ModelContextProtocol.AspNetCore;
using ModelContextProtocol.AspNetCore.Authentication;
using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Abstractions;

namespace MyBudget.Api.Installers;

public class MCPInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMcpServer()
            .WithHttpTransport(options =>
            {
                options.SessionMode = HttpServerSessionMode.Stateless;
            })
            .WithToolsFromAssembly();
    }

    public static void Use(WebApplication app)
    {
        app.MapMcp("/mcp")
            .RequireAuthorization(policy =>
            {
                policy.AuthenticationSchemes.Add(McpAuthenticationDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context =>
                    context.User.HasScope("my_budget.mcp")
                );
            });
    }
}