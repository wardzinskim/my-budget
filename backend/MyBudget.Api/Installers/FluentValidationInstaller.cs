using FluentValidation;
using MyBudget.Application.Weather.WeatherQuery;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Api.Installers;

public sealed class FluentValidationInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddValidatorsFromAssemblyContaining<WeatherQueryValidator>();
    }
}