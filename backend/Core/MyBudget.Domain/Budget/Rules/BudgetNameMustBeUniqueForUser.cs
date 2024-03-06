using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budget.Rules;

internal class BudgetNameMustBeUniqueForUser(string name, Guid userId) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(BudgetNameMustBeUniqueForUser),
            "Budget with this name already exists.");


    private readonly string _name = name;
    private readonly Guid _userId = userId;

    public Result Validate()
    {
        return Result.Failure(Error);
    }
}