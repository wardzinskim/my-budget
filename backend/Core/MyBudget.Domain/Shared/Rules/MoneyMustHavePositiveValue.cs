using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared.Rules;

internal class MoneyMustHavePositiveValue(decimal value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(MoneyMustHavePositiveValue),
            "Money value must be positive.");

    public ValueTask<Result> ValidateAsync(CancellationToken cancellationToken = default)
    {
        if (value >= 0) return ValueTask.FromResult(Result.Success());
        return ValueTask.FromResult(Result.Failure(Error));
    }
}