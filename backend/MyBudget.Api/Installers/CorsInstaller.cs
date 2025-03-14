using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Api.Installers;

public class CorsInstaller : IInstaller
{
    private record CorsConfig
    {
        public required string[] AllowedOrigins { get; init; }
    }

    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var corsConfig = configuration.GetSection(nameof(CorsConfig)).Get<CorsConfig>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(corsConfig!.AllowedOrigins);
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.AllowCredentials();
            });
        });
    }

    public static void Use(IApplicationBuilder app)
    {
        app.UseCors();
    }
}