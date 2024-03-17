using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.CreateBudget;
using MyBudget.Application.Budgets.CreateBudgetCategory;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;

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

        app.MapPost("/budget/category", CreateBudgetCategory)
          .WithName(nameof(CreateBudgetCategory))
          .WithTags("budget")
          .Produces(StatusCodes.Status201Created)
          .ProducesValidationProblem()
          .WithOpenApi()
          .IncludeInOpenApi();

    }

    private static async Task<IResult> CreateBudget(
        IMediator mediator,
        [FromBody] CreateBudgetCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(command, cancellationToken);

        return result.Match(Results.Created);
    }

    private static async Task<IResult> GetBudgets(
        IMediator mediator,
        [AsParameters] GetBudgetsQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(query, cancellationToken);

        return result.Match(x => Results.Ok(x));
    }

    private static async Task<IResult> CreateBudgetCategory(
       IMediator mediator,
       [FromBody] CreateBudgetCategoryCommand command,
       CancellationToken cancellationToken
   )
    {
        var result = await mediator.SendRequest(command, cancellationToken);

        return result.Match(Results.Created);
    }

}