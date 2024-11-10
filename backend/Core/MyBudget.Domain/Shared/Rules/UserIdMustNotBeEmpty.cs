using MyBudget.SharedKernel;

namespace MyBudget.Domain.Shared.Rules;

internal class UserIdMustNotBeEmpty(Guid value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(UserIdMustNotBeEmpty),
            "UserId must not be empty.");

    public Result Validate()
    {
        if (value != Guid.Empty) return Result.Success();
        return Result.Failure(Error);
    }
}