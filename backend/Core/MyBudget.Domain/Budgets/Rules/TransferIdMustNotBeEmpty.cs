using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class TransferIdMustNotBeEmpty(Guid value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(TransferIdMustNotBeEmpty),
            "TransferI must not be empty.");

    public Result Validate()
    {
        if (value != Guid.Empty) return Result.Success();
        return Result.Failure(Error);
    }
}