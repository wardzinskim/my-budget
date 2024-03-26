using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared.Rules;

internal class MoneyMustHaveCurrency(string currency) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(MoneyMustHaveCurrency),
            "Money value must have currency.");


    public Result Validate()
    {
        if (!string.IsNullOrWhiteSpace(currency)) return Result.Success();
        return Result.Failure(Error);
    }
}