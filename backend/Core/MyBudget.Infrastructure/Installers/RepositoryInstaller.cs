using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.Infrastructure.Domain.Budgets;
using MyBudget.Infrastructure.Domain.Budgets.Transfers;

namespace MyBudget.Infrastructure.Installers;

public sealed class RepositoryInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddTransient<IBudgetRepository, BudgetRepository>();
        services.AddTransient<IBudgetNameUniquenessChecker, BudgetNameUniquenessChecker>();
        services.AddTransient<ITransferRepository, TransferRepository>();
    }
}