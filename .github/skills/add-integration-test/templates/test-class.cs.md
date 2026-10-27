# Test class template

```csharp
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Features.Core;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class Post<Feature>Tests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task POST_budget_<feature>_no_budget_returns_404()
    {
        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{Guid.NewGuid()}/<route>",
            new <Feature>Request("asd"));

        //assert
        await AssertBudgetNotExistsAsync(response);
    }

    [Fact]
    public async Task POST_budget_<feature>_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget = FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/<route>",
            new <Feature>Request(faker.Random.String2(10)));

        //assert
        await AssertBudgetForbiddenAsync(response);
    }

    [Fact]
    public async Task POST_budget_<feature>_with_empty_name_returns_validation_error()
    {
        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{Guid.NewGuid()}/<route>",
            new <Feature>Request(null!));

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Contains(problemDetail.Errors.Keys, x => x == "name");
    }

    [Fact]
    public async Task POST_budget_<feature>_<happy_path>()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget = FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/<route>",
            new <Feature>Request(faker.Random.String2(10)));

        //assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        _dbContext.ChangeTracker.Clear();
        var saved = await _dbContext.Budgets.SingleOrDefaultAsync(x => x.Id == budgetId);
        Assert.NotNull(saved);
    }
}
```

Business-rule case: assert `problemDetail.Extensions["code"]!.ToString() == "<snake_case_rule_name>"` on a `ProblemDetails` body.
