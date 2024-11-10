using MyBudget.Domain.Budgets.Transfers;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class TransferMustExists(IEnumerable<Transfer> transfers, TransferId transferId) : IBusinessRule
{
    public Result Validate()
    {
        return transfers.Any(x => x.Id == transferId)
            ? Result.Success()
            : Result.Failure(BudgetsErrors.TransferNotFound);
    }
}