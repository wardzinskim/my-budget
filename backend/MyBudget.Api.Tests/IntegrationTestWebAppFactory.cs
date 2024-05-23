using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Infrastructure.Database;
using Testcontainers.MySql;

namespace MyBudget.Api.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer = new MySqlBuilder()
        .WithImage("mysql:8.3.0")
        .Build();

    public Guid UserId { get; set; } = Guid.Parse("261d2b6e-e53d-4d40-b1ec-234016d8f69b");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(service => typeof(DbContextOptions<BudgetContext>) == service.ServiceType);
            if (descriptor is not null)
                services.Remove(descriptor);

            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

            services.AddDbContextPool<BudgetContext>(dbContextOptions =>
            {
                dbContextOptions
                    .UseMySql(_mySqlContainer.GetConnectionString(), serverVersion, mysqlOptions =>
                    {
                        mysqlOptions.SchemaBehavior(
                            Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate,
                            (schema, table) => $"{schema}.{table}");
                    });
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.TestAuthScheme;
                    options.DefaultChallengeScheme = TestAuthHandler.TestAuthScheme;
                    options.DefaultScheme = TestAuthHandler.TestAuthScheme;
                })
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.TestAuthScheme,
                    options => { options.UserId = () => UserId; });
        });
    }


    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetContext>();
        await dbContext.Database.MigrateAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _mySqlContainer.StopAsync();
    }
}