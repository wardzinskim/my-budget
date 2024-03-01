using MassTransit;
using MyBudget.Api.Installers.Abstraction;
using MyBudget.Application.Weather.WeatherQuery;

namespace MyBudget.Api.Installers;

public sealed class MediatorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMediator(configure =>
        {
            configure.AddConsumers(typeof(WeatherQuery).Assembly);
        });
    }
}