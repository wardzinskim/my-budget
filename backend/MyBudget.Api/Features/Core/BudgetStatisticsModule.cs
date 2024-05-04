using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.GetBudgetTotals;

namespace MyBudget.Api.Features.Core;

public class BudgetStatisticsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/budget/{id:guid}/totals", GetBudgetTotals)
            .WithName(nameof(GetBudgetTotals))
            .WithTags("budget-statistics")
            .Produces(StatusCodes.Status200OK, typeof(BudgetTotals))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi()
            .IncludeInOpenApi();
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
}