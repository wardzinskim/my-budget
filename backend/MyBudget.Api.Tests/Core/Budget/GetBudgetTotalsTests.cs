using Bogus;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Application.Budgets.GetBudgetTotals;
using MyBudget.Domain.Budgets.Transfers;
using System.Globalization;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class GetBudgetTotalsTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task GET_budget_totals_no_budget_return_404()
    {
        //arrange
        var budgetId = Guid.NewGuid();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/totals");


        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task GET_budget_totals_is_not_my_budget_return_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/totals");


        //assert
        await AssertBudgetForbiddenAsync(response);
    }

    [Theory]
    [InlineData(null, null, 1111, 1111)]
    [InlineData(2023, null, 1, 1)]
    [InlineData(2024, null, 1110, 1110)]
    [InlineData(2024, 1, 110, 110)]
    [InlineData(2024, 12, 1000, 1000)]
    [InlineData(2025, 12, 0, 0)]
    public async Task GET_budget_totals_returns_totals(
        int? year,
        int? month,
        decimal expectedIncomes,
        decimal expectedExpenses
    )
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        var transfers = new[]
        {
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                new(faker.Random.String2(10), 1, "PLN", new DateTime(2023, 12, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                new(faker.Random.String2(10), 10, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                new(faker.Random.String2(10), 100, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                new(faker.Random.String2(10), 1000, "PLN", new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 1, "PLN", new DateTime(2023, 12, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 10, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 100, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc))),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 1000, "PLN", new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc)))
        }.Select(x => x.Value).ToList();

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.Transfers.AddRangeAsync(transfers);
        await _dbContext.SaveChangesAsync();

        Dictionary<string, string?> queryParams = new() {{"year", year?.ToString()}, {"month", month?.ToString()}};

        string url =
            $"/budget/{budgetId}/totals?{string.Join("&", queryParams.Where(x => x.Value is not null).Select(x => $"{x.Key}={x.Value}"))}";

        //act
        var response = await _httpClient.GetFromJsonAsync<BudgetTotals>(url);


        //assert
        Assert.NotNull(response);
        Assert.Equal(expectedExpenses, response.Expenses);
        Assert.Equal(expectedIncomes, response.Incomes);
    }
}