using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net;

namespace MyBudget.Api.Tests.Core.Budget.Transfers;

public class GetBudgetTransferTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task GET_budget_transfer_no_budgets_return_404()
    {
        //act
        var response = await _httpClient.GetAsync($"/budget/{Guid.NewGuid()}/transfer/{Guid.NewGuid()}");

        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task GET_budget_transfer_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}");

        //assert
        await AssertBudgetForbiddenAsync(response);
    }


    [Fact]
    public async Task GET_budget_transfer_transfer_not_exists_returns_404()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}");

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("transfer_not_found", problemDetail.Extensions["code"]!.ToString());
    }
}