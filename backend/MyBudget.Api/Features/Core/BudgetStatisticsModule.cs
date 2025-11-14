using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.GetBudgetTotals;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
using MyBudget.Application.Budgets.Model;

namespace MyBudget.Api.Features.Core;

public class BudgetStatisticsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/budget/{id:guid}")
            .WithTags("budget-statistics")
            .RequireAuthorization()
            .IncludeInOpenApi();

        group.MapGet("totals", GetBudgetTotals)
            .WithName(nameof(GetBudgetTotals))
            .Produces(StatusCodes.Status200OK, typeof(BudgetTotals))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        group.MapGet("/totals/grouped-by-category", GetBudgetTransfersTotalsGropedByCategory)
            .WithName(nameof(GetBudgetTransfersTotalsGropedByCategory))
            .Produces(StatusCodes.Status200OK, typeof(CategoryValue[]))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);
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

        return result.Match(Results.Ok);
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

        return result.Match(Results.Ok);
    }
}