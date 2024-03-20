using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Infrastructure.Abstraction.Installer;
using MyBudget.Infrastructure.Database;
using MyBudget.Infrastructure.Database.Interceptors;
using MyBudget.Infrastructure.Domain;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Installers;

public sealed class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

        services.AddSingleton<AuditableInterceptor>();

        services.AddDbContextPool<BudgetContext>((sp, dbContextOptions) =>
        {
            dbContextOptions
                .UseMySql(configuration.GetConnectionString("Default"), serverVersion, mysqlOptions =>
                {
                    mysqlOptions.SchemaBehavior(
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate,
                        (schema, table) => $"{schema}.{table}");
                });
            dbContextOptions.AddInterceptors(sp.GetRequiredService<AuditableInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}