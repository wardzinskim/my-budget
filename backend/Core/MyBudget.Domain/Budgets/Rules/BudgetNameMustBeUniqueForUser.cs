using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class BudgetNameMustBeUniqueForUser(string name, Guid userId, IBudgetNameUniquenessChecker budgetNameUniquenessChecker) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetNameMustBeUniqueForUser),
            "Budget with this name already exists.");


    private readonly string _name = name;
    private readonly Guid _userId = userId;

    public async ValueTask<Result> ValidateAsync(CancellationToken cancellationToken = default)
       => await budgetNameUniquenessChecker.IsUniqueAsync(_userId, _name, cancellationToken).ConfigureAwait(false) ? (Result)Error : Result.Success();

}