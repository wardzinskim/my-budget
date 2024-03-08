using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Infrastructure.Abstraction.Installer;
using MyBudget.Infrastructure.Database;
using MyBudget.Infrastructure.Domain;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Installers;

public sealed class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

        services.AddDbContextPool<BudgetContext>(dbContextOptions =>
        {
            dbContextOptions
                .UseMySql(configuration.GetConnectionString("Default"), serverVersion);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}