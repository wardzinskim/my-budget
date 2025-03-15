using MyBudget.Infrastructure.Abstractions.Installer;
using Quartz;
using Quartz.AspNetCore;

namespace MyBudget.Identity.Installers;

public class QuartzInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddQuartz(options =>
        {
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        services.AddQuartzServer(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }
}