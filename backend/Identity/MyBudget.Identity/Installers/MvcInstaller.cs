using MyBudget.Infrastructure.Abstractions.Installer;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;

namespace MyBudget.Identity.Installers;

public class MvcInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddAuthorization(x =>
        {
            x.AddPolicy("MyBudgetIdentity", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
                policy.RequireClaim(OpenIddictConstants.Claims.Private.Scope, "my_budget.identity");
            });
        });

        //TODO: for dev purpose only
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddControllersWithViews();
        services.AddRazorPages();
    }

    public void Use(WebApplication app)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
    }
}