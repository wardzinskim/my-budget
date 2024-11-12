using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Features.Core;
using MyBudget.Application.Budgets.Model;
using MyBudget.Domain.Budgets.Transfers;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget.Transfers;

public class PostBudgetTransferTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Theory]
    [InlineData(TransferDTOType.Income)]
    [InlineData(TransferDTOType.Expense)]
    public async Task POST_budget_transfer_create_transfer(TransferDTOType type)
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var expenseName = faker.Random.String2(10);
        decimal value = Math.Round(faker.Random.Decimal(), 2);
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        budget.AddTransferCategory("CATEGORY");

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(type, expenseName, value, currency, "CATEGORY", dateTime));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var transfer = await _dbContext.Transfers
            .AsNoTracking()
            .Where(x => x.BudgetId == budgetId)
            .FirstOrDefaultAsync();

        Assert.NotNull(transfer);
        Assert.Equal(budgetId, transfer.BudgetId);
        Assert.Equal(value, transfer.Value.Value);
        Assert.Equal(currency, transfer.Value.Currency);
        Assert.Equal(dateTime, transfer.TransferDate, TimeSpan.FromSeconds(1.0));
        Assert.Equal(expenseName, transfer.Name);
        Assert.Equal("CATEGORY", transfer.Category);
        Assert.Equal((TransferType)type, transfer.Type);
    }

    [Theory]
    [InlineData(TransferDTOType.Income)]
    [InlineData(TransferDTOType.Expense)]
    public async Task POST_budget_transfer_with_no_exists_transfer_category_returns_validation_errors(
        TransferDTOType type
    )
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var expenseName = faker.Random.String2(10);
        decimal value = Math.Round(faker.Finance.Random.Decimal(), 2);
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(type, expenseName, value, currency, "CATEGORY", dateTime));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status400BadRequest, problemDetail.Status);
        Assert.Equal("transfer_category_must_be_defined", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task POST_budget_transfer_no_budget_returns_404()
    {
        //arrange 
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var expenseName = faker.Random.String2(10);
        decimal value = Math.Round(faker.Random.Decimal(), 2);
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(TransferDTOType.Income, expenseName, value, currency, null, dateTime));

        //assert
        await AssertBudgetNotExistsAsync(response);
    }

    [Fact]
    public async Task POST_budget_transfer_create_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var expenseName = faker.Random.String2(10);
        decimal value = Math.Round(faker.Random.Decimal(), 2);
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(TransferDTOType.Income, expenseName, value, currency, null, dateTime));

        //assert
        await AssertBudgetForbiddenAsync(response);
    }
}