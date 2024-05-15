using Microsoft.AspNetCore.Identity;
using MyBudget.Identity.Data;
using MyBudget.Infrastructure.Abstraction.Installer;

namespace MyBudget.Identity.Installers;

public class IdentityInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services
           .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
           .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
