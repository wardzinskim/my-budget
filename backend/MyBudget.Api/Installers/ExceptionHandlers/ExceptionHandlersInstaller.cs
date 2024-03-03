using MyBudget.Api.Installers.Abstraction;

namespace MyBudget.Api.Installers.ExceptionHandlers;

public class ExceptionHandlersInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddProblemDetails();
    }
}