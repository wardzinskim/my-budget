using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyBudget.Infrastructure.Abstractions.Installer;

public interface IInstaller
{
    void Install(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostingEnvironment
    );
}