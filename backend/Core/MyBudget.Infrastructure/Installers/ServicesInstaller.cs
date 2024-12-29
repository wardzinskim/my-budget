using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBudget.Application.Budgets.Services;
using MyBudget.Application.Services;
using MyBudget.Identity.Contract;
using MyBudget.Infrastructure.Abstractions.Installer;
using MyBudget.Infrastructure.Application.Services;

namespace MyBudget.Infrastructure.Installers;

public class ServicesInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddTransient<IBudgetAccessValidator, BudgetAccessValidator>();
        services.AddTransient<IUserService, UserService>();

        services.AddGrpcClient<Users.UsersClient>((services, options) =>
        {
            options.Address = new Uri(configuration["MyBudget.Identity:ApiUrl"]!);
            options.ChannelOptionsActions.Add(channelOptions =>
            {
                channelOptions.Credentials = ChannelCredentials.Insecure;
            });
        });
    }
}