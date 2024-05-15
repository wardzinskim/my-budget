using Microsoft.EntityFrameworkCore;
using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Identity.Installers;

public class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

        services.AddDbContextPool<ApplicationDbContext>((sp, dbContextOptions) =>
        {
            dbContextOptions
                .UseMySql(configuration.GetConnectionString("Default"), serverVersion, mysqlOptions =>
                {
                    mysqlOptions.SchemaBehavior(
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate,
                        (schema, table) => $"{schema}.{table}");
                });

            dbContextOptions.UseOpenIddict();
        });



    }
}
