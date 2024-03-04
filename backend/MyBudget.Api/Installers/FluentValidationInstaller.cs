using FluentValidation;
using MyBudget.Api.Installers.Abstraction;
using MyBudget.Application.Weather.WeatherQuery;

namespace MyBudget.Api.Installers;

public sealed class FluentValidationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddValidatorsFromAssemblyContaining<WeatherQueryValidator>();
    }
}