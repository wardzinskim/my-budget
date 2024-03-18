using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class PutBudgetCategoryArchiveTests(IntegrationTestWebAppFactory application)
    : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task PUT_budget_category_archive_update_category_status()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var categoryName = faker.Random.String2(10);

        var budget =
            new Domain.Budgets.Budget(budgetId, _application.UserId, DateTime.Now, faker.Random.String2(10));
        budget.AddTransferCategory(categoryName);

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.PutAsync($"/budget/{budgetId}/category/{categoryName}/archive", null);


        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        _dbContext.ChangeTracker.Clear();
        budget = await _dbContext.Budgets
            .SingleOrDefaultAsync(x => x.Id == budgetId);

        Assert.NotNull(budget);
        Assert.Single(budget.Categories);
        Assert.Equal(categoryName, budget.Categories.First().Name);
        Assert.Equal(TransferCategoryStatus.Archived, budget.Categories.First().Status);
    }


    [Fact]
    public async Task PUT_budget_category_archive_no_budget_return_404()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var categoryName = faker.Random.String2(10);

        //act
        var response = await _httpClient.PutAsync($"/budget/{budgetId}/category/{categoryName}/archive", null);


        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("budget_not_found", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task PUT_budget_category_archive_no_category_return_404()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var categoryName = faker.Random.String2(10);

        var budget =
            new Domain.Budgets.Budget(budgetId, _application.UserId, DateTime.Now, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.PutAsync($"/budget/{budgetId}/category/{categoryName}/archive", null);

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("budget_category_not_found", problemDetail.Extensions["code"]!.ToString());
    }
}