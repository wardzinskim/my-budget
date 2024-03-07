using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.CreateBudget;

namespace MyBudget.Api.Features.Core;

public class BudgetModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/budget", CreateBudged)
            .WithName(nameof(CreateBudged))
            .WithTags("budget")
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithOpenApi()
            .IncludeInOpenApi();
    }

    private static async Task<IResult> CreateBudged(
        IMediator mediator,
        [FromBody] CreateBudgetCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(command, cancellationToken);

        return result.Match(Results.Created);
    }
}