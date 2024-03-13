using Microsoft.Extensions.DependencyInjection;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Api.Tests.Core;
public abstract class BudgetsIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>,
    IDisposable
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
