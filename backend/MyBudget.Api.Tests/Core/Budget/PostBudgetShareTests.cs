using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Features.Core;
using MyBudget.Application.Services;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class PostBudgetShareTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task POST_budget_share_success()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        application.UserService.User = new UserDto(userId, userLogin);

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        budget = await _dbContext.Budgets.FirstOrDefaultAsync(x => x.Id == budget.Id);
        Assert.NotNull(budget);
        Assert.NotEmpty(budget.Shares);
        Assert.Contains(budget.Shares, x => x.UserId == userId && x.UserName == userLogin);
    }

    [Fact]
    public async Task POST_budget_share_to_not_existing_user_returns_validation_error()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        application.UserService.User = null;

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.Equal("user_login_not_exists", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task POST_budget_share_returns_404()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        application.UserService.User = null;

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        await AssertBudgetNotExistsAsync(response);
    }

    [Fact]
    public async Task POST_budget_share_returns_403()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        application.UserService.User = new UserDto(userId, userLogin);

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        await AssertBudgetForbiddenAsync(response);
    }

    [Fact]
    public async Task POST_budget_share_to_owner_returns_validation_error()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        application.UserService.User = new UserDto(budget.OwnerId, userLogin);

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.Equal("budget_must_not_be_shared_to_owner", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task POST_budget_share_twice_returns_validation_error()
    {
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var userLogin = faker.Person.Email;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));
        budget.Share(userId, userLogin);
        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
        application.UserService.User = new UserDto(userId, userLogin);

        var response =
            await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/share", new ShareBudgetRequest(userLogin));

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.Equal("budget_is_already_shared", problemDetail.Extensions["code"]!.ToString());
    }
}