using Bogus;

namespace MyBudget.Api.Tests.Core.Budget;

public class GetBudgetTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task GET_budget_no_budget_return_404()
    {
        //arrange
        var budgetId = Guid.NewGuid();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}");


        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task GET_budget_no_budget_return_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        //act
        var response = await _httpClient.GetAsync($"/budget/{budgetId}");


        //assert
        await AssertBudgetForbiddenAsync(response);
    }
}