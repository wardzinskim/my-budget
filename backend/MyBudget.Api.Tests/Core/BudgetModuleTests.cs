namespace MyBudget.Api.Tests.Core;

public class BudgetModuleTests : BudgetsIntegrationTest
{
    public BudgetModuleTests(IntegrationTestWebAppFactory application)
        : base(application)
    {
    }


    [Fact]
    public async Task GET_budget_retrieves_all_user_budgets()
    {
        var response = await _httpClient.GetAsync("/budget");
        Assert.NotNull(response);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}