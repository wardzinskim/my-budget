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
                .UseSqlServer(configuration.GetConnectionString("Default"), configuration =>
                {
                    configuration.MigrationsHistoryTable("__EFMigrationsHistory", "identity");
                });

            dbContextOptions.UseOpenIddict();
        });
    }
}
