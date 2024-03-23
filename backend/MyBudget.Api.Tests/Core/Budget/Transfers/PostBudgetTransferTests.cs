using Bogus;
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
        decimal value = faker.Random.Decimal();
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(type, expenseName, value, currency, dateTime));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var transfer = await _dbContext.Budgets
            .AsNoTracking()
            .Where(x => x.Id == budgetId)
            .Select(x => x.Transfers.FirstOrDefault())
            .FirstOrDefaultAsync();

        Assert.NotNull(transfer);
        Assert.Equal(budgetId, transfer.BudgetId);
        Assert.Equal(value, transfer.Value.Value);
        Assert.Equal(currency, transfer.Value.Currency);
        Assert.Equal(dateTime, transfer.TransferDate, TimeSpan.FromSeconds(1.0));
        Assert.Equal(expenseName, transfer.Name);
        Assert.Equal((TransferType)type, transfer.Type);

    }

    [Fact]
    public async Task POST_budget_transfer_no_budget_returns_404()
    {
        //arrange 
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var expenseName = faker.Random.String2(10);
        decimal value = faker.Random.Decimal();
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
           new CreateTransferRequest(TransferDTOType.Income, expenseName, value, currency, dateTime));

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
        decimal value = faker.Random.Decimal();
        var currency = faker.Random.String2(3);
        var dateTime = DateTime.UtcNow;

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        //act
        var response = await _httpClient.PostAsJsonAsync($"/budget/{budgetId}/transfer",
            new CreateTransferRequest(TransferDTOType.Income, expenseName, value, currency, dateTime));

        //assert
        await AssertBudgetForbiddenAsync(response);

    }
}
