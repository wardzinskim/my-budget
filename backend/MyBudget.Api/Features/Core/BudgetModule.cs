using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.CreateBudget;
using MyBudget.Application.Budgets.GetBudget;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;

namespace MyBudget.Api.Features.Core;

public class BudgetModule : CarterModule
{
    public BudgetModule() : base("budget")
    {
        WithTags("budget");
        IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", CreateBudget).WithName(nameof(CreateBudget))
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapGet("", GetBudgets)
            .WithName(nameof(GetBudgets))
            .WithTags("budget")
            .Produces<IEnumerable<BudgetDTO>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        app.MapGet("{id:guid}", GetBudget)
            .WithName(nameof(GetBudget))
            .WithTags("budget")
            .Produces<BudgetDTO>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();
    }

    private static async Task<IResult> CreateBudget(
        IMediator mediator,
        [FromBody] CreateBudgetRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new CreateBudgetCommand(request.Name, request.Description),
            cancellationToken);

        return result.Match(x => Results.CreatedAtRoute(nameof(GetBudget), new {id = x}));
    }

    private static async Task<IResult> GetBudgets(
        IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new GetBudgetsQuery(), cancellationToken);

        return result.Match(x => Results.Ok(x));
    }

    private static async Task<IResult> GetBudget(
        [FromRoute] Guid id,
        IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new GetBudgetQuery(id), cancellationToken);

        return result.Match(x => Results.Ok(x));
    }
}