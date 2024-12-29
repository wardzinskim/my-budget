using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class UserNameMustNotBeEmpty(string userName) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(UserNameMustNotBeEmpty),
            "UserName must not be empty.");

    public Result Validate()
    {
        if (!string.IsNullOrWhiteSpace(userName)) return Result.Success();
        return Result.Failure(Error);
    }
}