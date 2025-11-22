using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.CreateBudget;
using MyBudget.Application.Budgets.GetBudget;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.ShareBudget;

namespace MyBudget.Api.Features.Core;

public class BudgetModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("budget")
            .WithTags("budget")
            .IncludeInOpenApi()
            .RequireAuthorization();

        group.MapPost("", CreateBudget).WithName(nameof(CreateBudget))
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapGet("", GetBudgets)
            .WithName(nameof(GetBudgets))
            .WithTags("budget")
            .Produces<IEnumerable<BudgetDTO>>()
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("{id:guid}", GetBudget)
            .WithName(nameof(GetBudget))
            .WithTags("budget")
            .Produces<BudgetDTO>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        group.MapPost("{id:guid}/share", ShareBudget).WithName(nameof(ShareBudget))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
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

        return result.Match(Results.Ok);
    }

    private static async Task<IResult> GetBudget(
        [FromRoute] Guid id,
        IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new GetBudgetQuery(id), cancellationToken);

        return result.Match(Results.Ok);
    }

    private static async Task<IResult> ShareBudget(
        [FromRoute] Guid id,
        [FromBody] ShareBudgetRequest request,
        IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(new ShareBudgetCommand(id, request.Login), cancellationToken);

        return result.Match(Results.NoContent);
    }
}