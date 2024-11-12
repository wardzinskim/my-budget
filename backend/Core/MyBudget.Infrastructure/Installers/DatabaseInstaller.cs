using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.Infrastructure.Database;
using MyBudget.Infrastructure.Database.Interceptors;
using MyBudget.Infrastructure.Domain;

namespace MyBudget.Infrastructure.Installers;

public sealed class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddSingleton<AuditableInterceptor>();

        services.AddDbContextPool<BudgetContext>((sp, dbContextOptions) =>
        {
            dbContextOptions
                .UseNpgsql(configuration.GetConnectionString("Default"), configuration =>
                {
                    configuration.MigrationsHistoryTable("__EFMigrationsHistory", SchemaName.Budget);
                })
                .UseSnakeCaseNamingConvention();

            dbContextOptions.AddInterceptors(sp.GetRequiredService<AuditableInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}