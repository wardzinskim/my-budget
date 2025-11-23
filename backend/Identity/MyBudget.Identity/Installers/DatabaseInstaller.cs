using Microsoft.EntityFrameworkCore;
using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Identity.Installers;

public class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddDbContextPool<ApplicationDbContext>((sp, dbContextOptions) =>
        {
            dbContextOptions
                .UseNpgsql(configuration.GetConnectionString("Default"), config =>
                {
                    config.MigrationsHistoryTable("__EFMigrationsHistory", "identity");
                })
                .UseSnakeCaseNamingConvention();

            dbContextOptions.UseOpenIddict();
        });
    }
}