using Microsoft.Extensions.DependencyInjection;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Api.Tests.Core;

[CollectionDefinition("Budgets Database collection")]
public class BudgetsDatabaseCollection : ICollectionFixture<IntegrationTestWebAppFactory>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Budgets Database collection")]
public abstract class BudgetsIntegrationTest : IDisposable
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient _httpClient;
    protected readonly BudgetContext _dbContext;
    protected readonly IntegrationTestWebAppFactory _application;

    protected BudgetsIntegrationTest(IntegrationTestWebAppFactory application)
    {
        _application = application;
        _httpClient = application.CreateClient();
        _scope = application.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<BudgetContext>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        _dbContext?.Dispose();
    }
}