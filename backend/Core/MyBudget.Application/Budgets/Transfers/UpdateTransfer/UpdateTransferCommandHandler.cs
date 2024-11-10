using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Abstractions.Features;

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

public sealed class UpdateTransferCommandHandler(
    IBudgetRepository budgetRepository,
    IBudgetAccessValidator budgetAccessValidator,
    ITransferRepository transferRepository
)
    : MediatorRequestHandler<UpdateTransferCommand, Result>
{
    protected override async Task<Result> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository
            .GetAsync(request.BudgetId, cancellationToken)
            .ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var transfer = await transferRepository.GetAsync(request.BudgetId, request.TransferId, cancellationToken);
        if (transfer is null) return BudgetsErrors.TransferNotFound;

        var result =
            transfer.Update(new(request.Name, request.Value, request.Currency, request.Date, request.Category));
        if (result.IsFailure) return result;

        return Result.Success();
    }
}