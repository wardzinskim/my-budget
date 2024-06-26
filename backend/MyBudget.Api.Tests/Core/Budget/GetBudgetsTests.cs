﻿using Bogus;
using MyBudget.Application.Budgets.Model;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget;

public class GetBudgetsTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task GET_budget_returns_no_budgets()
    {
        _application.UserId = Guid.NewGuid();

        var response = await _httpClient.GetAsync("/budget");
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var budgets = await response.Content.ReadFromJsonAsync<BudgetDTO[]>();
        Assert.NotNull(budgets);
    }

    [Fact]
    public async Task GET_budget_returns_budgets()
    {
        _application.UserId = Guid.NewGuid();
        var faker = new Faker();

        var budget =
            FakeBudgetBuilder.Build(Guid.NewGuid(), _application.UserId, faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();

        var response = await _httpClient.GetAsync("/budget");
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var budgets = await response.Content.ReadFromJsonAsync<BudgetDTO[]>(Constants.DefaultJsonSerializerOptions);

        Assert.NotNull(budgets);
        Assert.Single(budgets);
        Assert.Equal(budget.Name, budgets.Single().Name);
        Assert.Equal(budget.Id, budgets.Single().Id);
    }
}