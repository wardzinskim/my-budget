using MyBudget.Domain.Budgets.Transfers;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class TransferMustExistsBeforeDeletion(IEnumerable<Transfer> transfers, Guid transferId) : IBusinessRule
{
    public ValueTask<Result> ValidateAsync(CancellationToken cancellationToken = default)
    {
        return transfers.Any(x => x.Id == transferId)
            ? ValueTask.FromResult(Result.Success())
            : ValueTask.FromResult(Result.Failure(BudgetsErrors.TransferNotFound));
    }
}