﻿using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.ArchiveBudgetCategory;
using MyBudget.Application.Budgets.CreateBudget;
using MyBudget.Application.Budgets.CreateBudgetCategory;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.CreateExpense;

namespace MyBudget.Api.Features.Core;

public class BudgetModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/budget", CreateBudget)
            .WithName(nameof(CreateBudget))
            .WithTags("budget")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithOpenApi()
            .IncludeInOpenApi();

        app.MapGet("/budget", GetBudgets)
            .WithName(nameof(GetBudgets))
            .WithTags("budget")
            .Produces<IEnumerable<BudgetDTO>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .IncludeInOpenApi();

        app.MapPost("/budget/{id:guid}/category", CreateBudgetCategory)
            .WithName(nameof(CreateBudgetCategory))
            .WithTags("budget")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi()
            .IncludeInOpenApi();

        app.MapPut("/budget/{id:guid}/category/{name}/archive", ArchiveBudgetCategory)
            .WithName(nameof(ArchiveBudgetCategory))
            .WithTags("budget")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi()
            .IncludeInOpenApi();

        app.MapPost("/budget/{id:guid}/transfer", AddExpense)
            .WithName(nameof(AddExpense))
            .WithTags("budget")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi()
            .IncludeInOpenApi();
    }

    private static async Task<IResult> CreateBudget(
        IMediator mediator,
        [FromBody] CreateBudgetRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new CreateBudgetCommand(request.Name), cancellationToken);

        return result.Match(Results.Created);
    }

    private static async Task<IResult> GetBudgets(
        IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new GetBudgetsQuery(), cancellationToken);

        return result.Match(x => Results.Ok(x));
    }

    private static async Task<IResult> CreateBudgetCategory(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] CreateBudgetCategoryRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new CreateBudgetCategoryCommand(id, request.Name), cancellationToken);

        return result.Match(Results.Created);
    }


    private static async Task<IResult> ArchiveBudgetCategory(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromRoute] string name,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new ArchiveBudgetCategoryCommand(id, name), cancellationToken);

        return result.Match(Results.NoContent);
    }

    private static async Task<IResult> AddExpense(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] CreateTransferRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new CreateExpenseCommand(id, request.Name, request.Value, request.Currency, request.Date),
            cancellationToken);

        return result.Match(Results.Created);
    }
}