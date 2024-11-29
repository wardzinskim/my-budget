using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using MyBudget.Api.Extensions;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.CreateTransfer;
using MyBudget.Application.Budgets.Transfers.DeleteTransfer;
using MyBudget.Application.Budgets.Transfers.GetTransfer;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using MyBudget.Application.Budgets.Transfers.UpdateTransfer;

namespace MyBudget.Api.Features.Core;

public class BudgetTransferModule : CarterModule
{
    public BudgetTransferModule() : base("/budget/{id:guid}/transfer")
    {
        WithTags("transfer");
        IncludeInOpenApi();
        RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", AddTransfer)
            .WithName(nameof(AddTransfer))
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapGet("", GetTransfers)
            .WithName(nameof(GetTransfers))
            .Produces(StatusCodes.Status200OK, typeof(TransfersQueryResponse))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();

        app.MapDelete("{transferId:guid}", DeleteTransfer)
            .WithName(nameof(DeleteTransfer))
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();

        app.MapPut("{transferId:guid}", UpdateTransfer)
            .WithName(nameof(UpdateTransfer))
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapGet("{transferId:guid}", GetTransfer)
            .WithName(nameof(GetTransfer))
            .Produces(StatusCodes.Status200OK, typeof(TransferDTO))
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .WithOpenApi();
    }

    private static async Task<IResult> AddTransfer(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] CreateTransferRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new CreateTransferCommand(id, request.Type, request.Name, request.Value, request.Currency, request.Category,
                request.Date),
            cancellationToken);

        return result.Match(Results.Created);
    }

    private static async Task<IResult> GetTransfers(
        IMediator mediator,
        [AsParameters] GetTransfersRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new GetTransfersQuery(request.Id, request.Type, request.DateFrom, request.DateTo), cancellationToken);

        return result.Match(Results.Ok);
    }

    private static async Task<IResult> DeleteTransfer(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromRoute] Guid transferId,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new DeleteTransferCommand(id, transferId),
            cancellationToken);

        return result.Match(() => Results.Ok());
    }

    private static async Task<IResult> UpdateTransfer(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromRoute] Guid transferId,
        [FromBody] UpdateTransferRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new UpdateTransferCommand(id, transferId, request.Name, request.Value, request.Currency, request.Date,
                request.Category),
            cancellationToken);

        return result.Match(() => Results.Ok());
    }

    private static async Task<IResult> GetTransfer(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromRoute] Guid transferId,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new GetTransferQuery(id, transferId), cancellationToken);

        return result.Match(Results.Ok);
    }
}