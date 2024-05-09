using Bogus;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
using MyBudget.Domain.Budgets.Transfers;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class GetBudgetTotalsGroupedByCategoryTests(IntegrationTestWebAppFactory application)
    : BudgetsIntegrationTest(application)
{
    [Theory]
    [InlineData(TransferType.Income)]
    [InlineData(TransferType.Expense)]
    public async Task GET_budget_totals_grouped_by_category_no_budget_return_404(TransferType type)
    {
        //arrange
        var budgetId = Guid.NewGuid();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/totals/grouped-by-category?type={type}");


        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Theory]
    [InlineData(TransferType.Income)]
    [InlineData(TransferType.Expense)]
    public async Task GET_budget_totals_grouped_by_category_is_not_my_budget_return_403(TransferType type)
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}/totals/grouped-by-category?type={type}");


        //assert
        await AssertBudgetForbiddenAsync(response);
    }


    [Fact]
    public async Task GET_budget_totals_grouped_by_category_returns_totals()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        string cat1 = "CAT1", cat2 = "CAT2";

        budget.AddTransferCategory(cat1);
        budget.AddTransferCategory(cat2);

        budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
            new(faker.Random.String2(10), 1, "PLN", new DateTime(2023, 12, 1), cat1));
        budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
            new(faker.Random.String2(10), 10, "PLN", new DateTime(2024, 1, 1), cat1));
        budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
            new(faker.Random.String2(10), 100, "PLN", new DateTime(2024, 1, 1), cat2));
        budget.AddTransfer(new IdGeneratorMock(Guid.NewGuid()), TransferType.Income,
            new(faker.Random.String2(10), 1000, "PLN", new DateTime(2024, 12, 1)));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response =
            await _httpClient.GetFromJsonAsync<CategoryValue[]>(
                $"/budget/{budgetId}/totals/grouped-by-category?type={TransferType.Income}");

        //assert
        Assert.NotNull(response);
        Assert.Equal(3, response.Length);

        Assert.NotNull(response.FirstOrDefault(x => x.Category == cat1));
        Assert.NotNull(response.FirstOrDefault(x => x.Category == cat2));
        Assert.NotNull(response.FirstOrDefault(x => x.Category == null));

        Assert.Equal(11, response.First(x => x.Category == cat1).Value);
        Assert.Equal(100, response.First(x => x.Category == cat2).Value);
        Assert.Equal(1000, response.First(x => x.Category == null).Value);
    }
}