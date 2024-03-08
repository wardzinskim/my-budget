using MyBudget.Api.SharedKernel;
using MyBudget.Infrastructure.Abstraction.Installer;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Installers;

public sealed class SharedKernelInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddScoped<IIdGenerator, IdGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}