using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.UpdateTransfer;

public record UpdateTransferCommand(
    Guid BudgetId,
    Guid TransferId,
    string Name,
    decimal Value,
    string Currency,
    DateTime Date,
    string? Category = null
)
    : Request<Result>, ICommand;

public sealed class UpdateTransferCommandHandler(IBudgetRepository budgetRepository, IRequestContext requestContext)
    : MediatorRequestHandler<UpdateTransferCommand, Result>
{
    protected override async Task<Result> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository
            .GetAsync(request.BudgetId, x => x.Id == request.TransferId, cancellationToken).ConfigureAwait(false);
        if (budget is null)
            return BudgetsErrors.BudgetNotFound;

        var access = budget.HasAccess(requestContext.UserId);
        if (access.IsFailure)
        {
            return access.Error;
        }

        var result = budget.UpdateTransfer(request.TransferId,
            new(request.Name, request.Value, request.Currency, request.Date, request.Category));
        if (result.IsFailure) return result;

        return Result.Success();
    }
}