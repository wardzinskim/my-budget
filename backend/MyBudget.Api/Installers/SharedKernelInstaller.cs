using MyBudget.Api.SharedKernel;
using MyBudget.Application.Abstraction;
using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Installers;

public sealed class SharedKernelInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddScoped<IIdGenerator, IdGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IRequestContext, RequestContest>();
    }
}