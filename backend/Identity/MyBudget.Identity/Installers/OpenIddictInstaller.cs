using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Identity.Installers;

public class OpenIddictInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options
                    .UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<Guid>();
            });
    }
}
