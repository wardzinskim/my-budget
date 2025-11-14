using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.ArchiveBudgetCategory;
using MyBudget.Application.Budgets.CreateBudgetCategory;

namespace MyBudget.Api.Features.Core;

public class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/budget/{id:guid}/category")
            .WithTags("category")
            .IncludeInOpenApi()
            .RequireAuthorization();

        group.MapPost("", CreateBudgetCategory)
            .WithName(nameof(CreateBudgetCategory))
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem();

        group.MapPut("/{name}/archive", ArchiveBudgetCategory)
            .WithName(nameof(ArchiveBudgetCategory))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem();
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
}