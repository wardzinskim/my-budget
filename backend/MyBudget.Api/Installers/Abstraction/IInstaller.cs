namespace MyBudget.Api.Installers.Abstraction;

public interface IInstaller
{
    void Install(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostingEnvironment
    );

    void Use(WebApplication app) { }
}