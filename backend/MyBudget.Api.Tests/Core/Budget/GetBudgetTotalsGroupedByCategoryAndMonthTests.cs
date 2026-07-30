using Bogus;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategoryAndMonth;
using MyBudget.Domain.Budgets.Transfers;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class GetBudgetTotalsGroupedByCategoryAndMonthTests(IntegrationTestWebAppFactory application)
    : BudgetsIntegrationTest(application)
{
    [Theory]
    [InlineData(TransferType.Income)]
    [InlineData(TransferType.Expense)]
    public async Task GET_budget_totals_grouped_by_category_and_month_no_budget_return_404(TransferType type)
    {
        //arrange
        var budgetId = Guid.NewGuid();

        //act
        var response = await _httpClient.GetAsync(
            $"/budget/{budgetId}/totals/grouped-by-category-and-month?type={type}&year=2024");


        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Theory]
    [InlineData(TransferType.Income)]
    [InlineData(TransferType.Expense)]
    public async Task GET_budget_totals_grouped_by_category_and_month_is_not_my_budget_return_403(TransferType type)
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.GetAsync(
            $"/budget/{budgetId}/totals/grouped-by-category-and-month?type={type}&year=2024");


        //assert
        await AssertBudgetForbiddenAsync(response);
    }


    [Fact]
    public async Task GET_budget_totals_grouped_by_category_and_month_returns_totals()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        string cat1 = "CAT1", cat2 = "CAT2";

        budget.AddTransferCategory(cat1);
        budget.AddTransferCategory(cat2);

        var transfers = new[]
        {
            // 2023 - should be excluded (different year)
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 1, "PLN", new DateTime(2023, 12, 1, 0, 0, 0, DateTimeKind.Utc), cat1)),
            // 2024 January - cat1
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 10, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), cat1)),
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 5, "PLN", new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc), cat1)),
            // 2024 January - cat2
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 100, "PLN", new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), cat2)),
            // 2024 December - no category
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Expense,
                new(faker.Random.String2(10), 1000, "PLN", new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc))),
            // 2024 December - Income should be excluded (different type)
            budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
                new(faker.Random.String2(10), 9999, "PLN", new DateTime(2024, 12, 1, 0, 0, 0, DateTimeKind.Utc), cat1)),
        }.Select(x => x.Value);

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.Transfers.AddRangeAsync(transfers);
        await _dbContext.SaveChangesAsync();


        //act
        var response =
            await _httpClient.GetFromJsonAsync<CategoryMonthValue[]>(
                $"/budget/{budgetId}/totals/grouped-by-category-and-month?type={TransferType.Expense}&year=2024");

        //assert
        Assert.NotNull(response);
        Assert.Equal(3, response.Length);

        Assert.Equal(15, response.Single(x => x.Month == 1 && x.Category == cat1).Value);
        Assert.Equal(100, response.Single(x => x.Month == 1 && x.Category == cat2).Value);
        Assert.Equal(1000, response.Single(x => x.Month == 12 && x.Category == null).Value);
    }

    [Fact]
    public async Task GET_budget_totals_grouped_by_category_and_month_returns_empty_array_when_no_transfers()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response =
            await _httpClient.GetFromJsonAsync<CategoryMonthValue[]>(
                $"/budget/{budgetId}/totals/grouped-by-category-and-month?type={TransferType.Expense}&year=2024");

        //assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }
}