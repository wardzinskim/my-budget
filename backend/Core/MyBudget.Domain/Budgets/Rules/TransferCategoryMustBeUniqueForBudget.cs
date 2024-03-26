using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class TransferCategoryMustBeUniqueForBudget(string name, IEnumerable<TransferCategory> transferCategories)
    : IBusinessRule
{
    private readonly string _name = name ?? throw new ArgumentNullException(nameof(name));

    private readonly IEnumerable<TransferCategory> _transferCategories =
        transferCategories ?? throw new ArgumentNullException(nameof(transferCategories));

    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(TransferCategoryMustBeUniqueForBudget),
            "Category with this name already exists.");


    public Result Validate()
    {
        return _transferCategories.Any(x => x.Name == _name) ? Result.Failure(Error) : Result.Success();
    }
}