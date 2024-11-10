using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class BudgetNameMustBeUniqueForUser(
    string name,
    UserId userId,
    IBudgetNameUniquenessChecker budgetNameUniquenessChecker
) : IAsyncBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetNameMustBeUniqueForUser),
            "Budget with this name already exists.");


    public async Task<Result> ValidateAsync(CancellationToken cancellationToken = default)
        => await budgetNameUniquenessChecker.IsUniqueAsync(userId, name, cancellationToken).ConfigureAwait(false)
            ? (Result)Error
            : Result.Success();
}