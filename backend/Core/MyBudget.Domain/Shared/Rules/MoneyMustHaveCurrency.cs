using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared.Rules;

internal class MoneyMustHaveCurrency(string currency) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(MoneyMustHaveCurrency),
            "Money value must have currency.");

    public ValueTask<Result> ValidateAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(currency)) return ValueTask.FromResult(Result.Success());

        return ValueTask.FromResult(Result.Failure(Error));
    }
}