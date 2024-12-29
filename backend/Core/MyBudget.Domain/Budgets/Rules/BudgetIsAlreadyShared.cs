using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class BudgetIsAlreadyShared(IEnumerable<BudgetShare> sharings, Guid userId) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetIsAlreadyShared),
            $"Budget is already shared with selected user");

    public Result Validate()
    {
        if (sharings.Any(x => x.UserId == userId)) return Result.Failure(Error);
        return Result.Success();
    }
}