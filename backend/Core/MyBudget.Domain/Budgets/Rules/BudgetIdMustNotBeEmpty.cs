using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class BudgetIdMustNotBeEmpty(Guid value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetIdMustNotBeEmpty),
            "BudgetId must not be empty.");

    public Result Validate()
    {
        if (value != Guid.Empty) return Result.Success();
        return Result.Failure(Error);
    }
}