using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyBudget.Infrastructure.Abstraction.Installer;

public interface IInstaller
{
    void Install(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostingEnvironment
    );

    void Use(WebApplication app) { }
}