using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared.Rules;

internal class MoneyMustHavePositiveValue(decimal value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(MoneyMustHavePositiveValue),
            "Money value must be positive.");

    public Result Validate()
    {
        if (value >= 0) return Result.Success();
        return Result.Failure(Error);
    }
}