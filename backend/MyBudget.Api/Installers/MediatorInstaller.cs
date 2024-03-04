using MassTransit;
using MassTransit.Middleware;
using MyBudget.Api.Installers.Abstraction;
using MyBudget.Api.Installers.MediatorFilters;
using MyBudget.Application.Weather.WeatherQuery;

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