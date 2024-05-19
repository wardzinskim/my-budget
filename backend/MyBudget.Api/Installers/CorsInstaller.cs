using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Api.Installers;

public class CorsInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });
    }

    public void Use(WebApplication app)
    {
        app.UseCors();
    }
}
