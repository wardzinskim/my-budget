﻿using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudget.Api.Features.Core;
using MyBudget.Api.Tests.Mocks;
using MyBudget.Domain.Budgets.Transfers;
using System.Net;
using System.Net.Http.Json;

namespace MyBudget.Api.Tests.Core.Budget.Transfers;

public class PutBudgetTransferTests(IntegrationTestWebAppFactory application) : BudgetsIntegrationTest(application)
{
    [Fact]
    public async Task PUT_budget_transfers_no_budgets_return_404()
    {
        //arrange
        var faker = new Faker();

        //act
        var response = await _httpClient.PutAsJsonAsync($"/budget/{Guid.NewGuid()}/transfer/{Guid.NewGuid()}",
            new UpdateTransferRequest(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.UtcNow));

        //assert
        await AssertBudgetNotExistsAsync(response);
    }


    [Fact]
    public async Task PUT_budget_transfer_is_not_my_budget_returns_403()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, Guid.NewGuid(), faker.Random.String2(10));

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.PutAsJsonAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}",
            new UpdateTransferRequest(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.UtcNow));

        //assert
        await AssertBudgetForbiddenAsync(response);
    }


    [Fact]
    public async Task PUT_budget_transfer_no_transfer_return_404()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));


        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.SaveChangesAsync();


        //act
        var response = await _httpClient.PutAsJsonAsync($"/budget/{budgetId}/transfer/{Guid.NewGuid()}",
            new UpdateTransferRequest(faker.Random.String2(10), faker.Random.Decimal(), "PLN", DateTime.UtcNow));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("transfer_not_found", problemDetail.Extensions["code"]!.ToString());
    }


    [Fact]
    public async Task PUT_budget_transfer_successfully_updated()
    {
        //arrange
        var faker = new Faker();
        var budgetId = Guid.NewGuid();
        var transferId = Guid.NewGuid();
        var requestBody =
            new UpdateTransferRequest(faker.Random.String2(10), Math.Round(faker.Random.Decimal(), 2), "PLN",
                DateTime.UtcNow,
                "CATEGORY");

        var budget =
            FakeBudgetBuilder.Build(budgetId, _application.UserId, faker.Random.String2(10));

        var transfer = budget.AddTransfer(new IdGeneratorMock(transferId), TransferType.Income,
            new(faker.Random.String2(10), faker.Random.Decimal(), "USD", DateTime.UtcNow));
        budget.AddTransferCategory("CATEGORY");

        await _dbContext.Budgets.AddAsync(budget);
        await _dbContext.Transfers.AddAsync(transfer.Value);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();


        //act
        var response = await _httpClient.PutAsJsonAsync($"/budget/{budgetId}/transfer/{transferId}", requestBody);


        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        budget = await _dbContext.Budgets
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == budgetId);

        var transfers = await _dbContext.Transfers.Where(x => x.BudgetId == budgetId)
            .ToListAsync();

        Assert.NotNull(budget);
        Assert.Single(transfers);
        Assert.Equal(transferId, transfers.Single().Id);
        Assert.Equal(requestBody.Name, transfers.Single().Name);
        Assert.Equal(requestBody.Value, transfers.Single().Value.Value);
        Assert.Equal(requestBody.Currency, transfers.Single().Value.Currency);
        Assert.Equal(requestBody.Date, transfers.Single().TransferDate, TimeSpan.FromSeconds(1.0));
        Assert.Equal(requestBody.Category, transfers.Single().Category);
    }
}