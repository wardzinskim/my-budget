using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.GetBudgetTotals;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
using MyBudget.Application.Budgets.Model;

namespace MyBudget.Api.Features.Core;

public class BudgetStatisticsModule : CarterModule
{
    public BudgetStatisticsModule() : base("/budget/{id:guid}")
    {
        WithTags("budget-statistics");
        IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("totals", GetBudgetTotals)
            .WithName(nameof(GetBudgetTotals))
            .Produces(StatusCodes.Status200OK, typeof(BudgetTotals))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();

        app.MapGet("/totals/grouped-by-category", GetBudgetTransfersTotalsGropedByCategory)
            .WithName(nameof(GetBudgetTransfersTotalsGropedByCategory))
            .Produces(StatusCodes.Status200OK, typeof(CategoryValue[]))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();
    }


    private static async Task<IResult> GetBudgetTotals(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromQuery] int? year,
        [FromQuery] int? month,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new GetBudgetTotalsQuery(id, year, month), cancellationToken);

        return result.Match((x) => Results.Ok(x));
    }

    private static async Task<IResult> GetBudgetTransfersTotalsGropedByCategory(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromQuery] TransferDTOType type,
        [FromQuery] int? year,
        [FromQuery] int? month,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new GetBudgetTransfersTotalsGroupedByCategoryQuery(id, type, year, month), cancellationToken);

        return result.Match((x) => Results.Ok(x));
    }
}