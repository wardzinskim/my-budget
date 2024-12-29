using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

public class BudgetMustNotBeSharedToOwner(UserId ownerId, Guid userId) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetMustNotBeSharedToOwner),
            $"Budget cannot be shared with owner");

    public Result Validate()
    {
        if (ownerId == userId) return Result.Failure(Error);
        return Result.Success();
    }
}