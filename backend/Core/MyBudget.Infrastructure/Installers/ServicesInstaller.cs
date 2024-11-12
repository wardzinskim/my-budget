using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Application.Budgets.Services;
using MyBudget.Infrastructure.Abstractions.Installer;

namespace MyBudget.Infrastructure.Installers;

public class ServicesInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddTransient<IBudgetAccessValidator, BudgetAccessValidator>();
    }
}