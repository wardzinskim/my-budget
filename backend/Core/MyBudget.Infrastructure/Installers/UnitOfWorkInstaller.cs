using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Infrastructure.Abstraction.Installer;
using MyBudget.Infrastructure.Domain;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Installers;

internal class UnitOfWorkInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}