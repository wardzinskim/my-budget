using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Domain.Budgets.Transfers;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget.Transfers;

public class DeleteBudgetTransferTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task DELETE_budget_transfers_no_budgets_return_404()
    {
        //act
        var response = await _httpClient.DeleteAsync($"/budget/{Guid.NewGuid()}/transfer/{Guid.NewGuid()}");

        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task DELETE_budget_transfer_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.DeleteAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}");

        //assert
        await AssertBudgetForbiddenAsync(response);
    }


    [Fact]
    public async Task DELETE_budget_transfer_no_transfer_return_404()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));


        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.DeleteAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}");

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("transfer_not_found", problemDetail.Extensions["code"]!.ToString());
    }


    [Fact]
    public async Task DELETE_budget_transfer_successfully_deleted()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var transferId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        var transfers =
            new[]
            {
                budget.AddTransfer(new IdGeneratorMock(transferId), TransferType.Income,
                    new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.UtcNow)),
                budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                    new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.UtcNow))
            }.Select(x => x.Value).ToList();

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.Transfers.AddRangeAsync(transfers);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();


        //act
        var response = await _httpClient.DeleteAsync($"/budget/{budgetId}/transfer/{transferId}");

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        budget = await _dbContext.Budgets
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == budgetId);

        transfers = await _dbContext.Transfers
            .Where(x => x.BudgetId == budgetId)
            .ToListAsync();

        Assert.NotNull(budget);
        Assert.Single(transfers);
        Assert.NotEqual(transferId, transfers.Single().Id);
    }
}