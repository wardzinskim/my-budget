using MassTransit;
using MyBudget.Api.Installers.MediatorFilters;
using MyBudget.Application.Weather.WeatherQuery;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Api.Installers;

public sealed class MediatorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMediator(configure =>
        {
            configure.AddConsumers(typeof(WeatherQuery).Assembly);

            configure.ConfigureMediator((context, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(ValidationFilter<>), context);
            });
        });
    }
}