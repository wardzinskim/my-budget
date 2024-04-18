using Bogus;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget.Transfers;

public class GetBudgetTransferTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task GET_budget_transfers_no_budgets_return_404()
    {
        //act
        var response = await _httpClient.GetAsync($"/budget/{Guid.NewGuid()}/transfer");

        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task GET_budget_transfers_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/transfer");

        //assert
        await AssertBudgetForbiddenAsync(response);
    }

    [Fact]
    public async Task GET_budget_transfers_no_query_parameters_return_last_30_days()
    {
        //arrange 
        var faker = new Faker();
        Guid budgetId = Guid.NewGuid(),
            transfer1 = Guid.NewGuid(),
            transfer2 = Guid.NewGuid();


        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        budget.AddTransfer(new IdGeneratorMock(transfer1), Domain.Budgets.Transfers.TransferType.Expense,
            new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.Now.AddDays(-1)));
        budget.AddTransfer(new IdGeneratorMock(transfer2), Domain.Budgets.Transfers.TransferType.Expense,
            new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.Now.AddDays(-40)));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act

        var response = await _httpClient.GetFromJsonAsync<TransfersQueryResponse>($"/budget/{budgetId}/transfer",
            Constants.DefaultJsonSerializerOptions);

        //assert
        Assert.NotNull(response);
        Assert.Equal(DateTime.UtcNow.Date.AddDays(1), response.DateTo);
        Assert.Equal(DateTime.UtcNow.Date.AddDays(1).AddDays(-30), response.DateFrom);
        Assert.NotEmpty(response.Transfers);
        Assert.Single(response.Transfers);
        Assert.Equal(transfer1, response.Transfers.Single().Id);
    }

    [Theory]
    [InlineData(TransferDTOType.Income)]
    [InlineData(TransferDTOType.Expense)]
    public async Task GET_budget_transfers_filter_by_type(TransferDTOType type)
    {
        //arrange 
        var faker = new Faker();
        Guid budgetId = Guid.NewGuid(),
            transfer1 = Guid.NewGuid(),
            transfer2 = Guid.NewGuid();


        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        budget.AddTransfer(new IdGeneratorMock(transfer1), Domain.Budgets.Transfers.TransferType.Income,
            new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.Now.AddDays(-1)));
        budget.AddTransfer(new IdGeneratorMock(transfer2), Domain.Budgets.Transfers.TransferType.Expense,
            new(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.Now.AddDays(-1)));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act

        var response = await _httpClient.GetFromJsonAsync<TransfersQueryResponse>(
            $"/budget/{budgetId}/transfer?type={type.ToString()}", Constants.DefaultJsonSerializerOptions);

        //assert
        Assert.NotNull(response);
        Assert.Equal(DateTime.UtcNow.Date.AddDays(1), response.DateTo);
        Assert.Equal(DateTime.UtcNow.Date.AddDays(1).AddDays(-30), response.DateFrom);
        Assert.NotEmpty(response.Transfers);
        Assert.Single(response.Transfers);
        Assert.Equal(type, response.Transfers.Single().Type);
    }
}