using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.CreateBudget;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class PostBudgetTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task POST_budget_create_budget()
    {
        var faker = new Faker();
        var budgetName = faker.Random.String2(50);

        var response = await _httpClient.PostAsJsonAsync("/budget", new CreateBudgetCommand(budgetName));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var budget = await _dbContext.Budgets.FirstOrDefaultAsync(x => x.Name == budgetName);
        Assert.NotNull(budget);
        Assert.Equal(_application.UserId, budget.OwnerId);
    }

    [Fact]
    public async Task POST_budget_with_too_long_name_returns_validation_error()
    {
        var faker = new Faker();
        var budgetName = faker.Random.String2(501);

        var response = await _httpClient.PostAsJsonAsync("/budget", new CreateBudgetCommand(budgetName));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.NotEmpty(problemDetail.Errors);
        Assert.Contains(problemDetail.Errors.Keys, x => x == "Name");
    }

    [Fact]
    public async Task POST_budget_with_empty_name_returns_validation_error()
    {
        var response = await _httpClient.PostAsJsonAsync("/budget", new CreateBudgetCommand(string.Empty));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.NotEmpty(problemDetail.Errors);
        Assert.Contains(problemDetail.Errors.Keys, x => x == "Name");
    }

    [Fact]
    public async Task POST_budget_with_duplicated_name_return_validation_error()
    {
        var faker = new Faker();
        var budgetName = faker.Random.String2(50);

        var response = await _httpClient.PostAsJsonAsync("/budget", new CreateBudgetCommand(budgetName));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        response = await _httpClient.PostAsJsonAsync("/budget", new CreateBudgetCommand(budgetName));
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.Equal("budget_name_must_be_unique_for_user", problemDetail.Extensions["code"]!.ToString());
    }
}