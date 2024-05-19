using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.Infrastructure.Domain.Budgets;

namespace MyBudget.Infrastructure.Installers;

public sealed class RepositoryInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddTransient<IBudgetNameUniquenessChecker, BudgetNameUniquenessChecker>();
    }
}