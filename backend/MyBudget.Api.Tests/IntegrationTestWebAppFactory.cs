using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Application.Services;
using MyBudget.Infrastructure.Database;
using OpenIddict.Abstractions;
using Testcontainers.PostgreSql;

namespace MyBudget.Api.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.4-alpine3.20")
        .Build();

    public Guid UserId { get; set; } = Guid.Parse("261d2b6e-e53d-4d40-b1ec-234016d8f69b");
    public UserServiceMock UserService { get; } = new UserServiceMock();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(service => typeof(DbContextOptions<BudgetContext>) == service.ServiceType);
            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContextPool<BudgetContext>(dbContextOptions =>
            {
                dbContextOptions
                    .UseNpgsql(_postgreSqlContainer.GetConnectionString(), postgresoptions =>
                    {
                        postgresoptions.MigrationsAssembly(typeof(BudgetContext).Assembly.FullName);
                    })
                    .UseSnakeCaseNamingConvention();
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.TestAuthScheme;
                    options.DefaultChallengeScheme = TestAuthHandler.TestAuthScheme;
                    options.DefaultScheme = TestAuthHandler.TestAuthScheme;
                })
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.TestAuthScheme,
                    options => { options.UserId = () => UserId; });

            // Production default policy is bound to the OpenIddict scheme; bind it to the test scheme instead.
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(TestAuthHandler.TestAuthScheme)
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context => context.User.HasScope("my_budget.api"))
                    .Build();
            });

            services.AddTransient<IUserService>(_ => UserService);
        });
    }


    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetContext>();
        await dbContext.Database.MigrateAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
    }
}