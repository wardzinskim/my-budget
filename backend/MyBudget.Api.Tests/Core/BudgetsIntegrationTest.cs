using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Infrastructure.Database;
using System.Net;
using System.Net.Http.Json;

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

    protected async Task AssertBudgetNotExistsAsync(HttpResponseMessage? response)
    {
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("budget_not_found", problemDetail.Extensions["code"]!.ToString());
    }

    protected async Task AssertBudgetForbiddenAsync(HttpResponseMessage? response)
    {
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status403Forbidden, problemDetail.Status);
        Assert.Equal("budget_access_denied", problemDetail.Extensions["code"]!.ToString());
    }
}