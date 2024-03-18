using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.CreateBudgetCategory;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class PostBudgetCategoryTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task POST_budget_category_no_budget_returns_404()
    {
        //act
        var response = await _httpClient.PostAsJsonAsync("/budget/category",
            new CreateBudgetCategoryCommand(Guid.NewGuid(), "asd"));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("budget_not_found", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task POST_budget_category_with_too_long_name_returns_validation_error()
    {
        //arrange
        var faker = new Faker();
        var categoryName = faker.Random.String(501);

        //act
        var response = await _httpClient.PostAsJsonAsync("/budget/category",
            new CreateBudgetCategoryCommand(Guid.NewGuid(), categoryName));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.NotEmpty(problemDetail.Errors);
        Assert.Contains(problemDetail.Errors.Keys, x => x == "Name");
    }


    [Fact]
    public async Task POST_budget_category_with_empty_name_returns_validation_error()
    {
        //act
        var response = await _httpClient.PostAsJsonAsync("/budget/category",
            new CreateBudgetCategoryCommand(Guid.NewGuid(), null!));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.NotEmpty(problemDetail.Errors);
        Assert.Contains(problemDetail.Errors.Keys, x => x == "Name");
    }


    [Fact]
    public async Task POST_budget_category_crate_category()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var categoryName = faker.Random.String(10);

        var budget =
            new Domain.Budgets.Budget(budgetId, _application.UserId, DateTime.Now, faker.Random.String(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.PostAsJsonAsync("/budget/category",
            new CreateBudgetCategoryCommand(budgetId, categoryName));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        budget = await _dbContext.Budgets.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == budgetId);

        Assert.NotNull(budget);
        Assert.Single(budget.Categories);
        Assert.Equal(categoryName, budget.Categories.First().Name);
        Assert.Equal(Domain.Budgets.TransferCategoryStatus.Active, budget.Categories.First().Status);
    }
}