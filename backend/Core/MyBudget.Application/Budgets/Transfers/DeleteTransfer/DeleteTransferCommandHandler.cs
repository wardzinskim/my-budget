using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.Transfers.DeleteTransfer;

public record DeleteTransferCommand(Guid BudgetId, Guid TransferId) : Request<Result>, ICommand;

public sealed class DeleteTransferCommandHandler(
    IBudgetRepository budgetRepository,
    ITransferRepository transferRepository,
    IBudgetAccessValidator budgetAccessValidator
)
    : MediatorRequestHandler<DeleteTransferCommand, Result>
{
    protected override async Task<Result> Handle(DeleteTransferCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository
            .GetAsync(request.BudgetId, cancellationToken)
            .ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var transfer = await transferRepository.GetAsync(request.BudgetId, request.TransferId, cancellationToken);
        if (transfer is null) return BudgetsErrors.TransferNotFound;

        var result = transfer.Delete();
        if (result.IsFailure) return result.Error;

        return Result.Success();
    }
}