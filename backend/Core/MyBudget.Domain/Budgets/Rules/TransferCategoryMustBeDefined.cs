using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Rules;

internal class TransferCategoryMustBeDefined(IEnumerable<TransferCategory> transferCategories, string? category)
    : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(TransferCategoryMustBeDefined),
            "Selected category is not defined in budget");

    public Result Validate()
    {
        return category is null ||
               transferCategories.Any(x => x.Name == category && x.Status == TransferCategoryStatus.Active)
            ? Result.Success()
            : Result.Failure(Error);
    }
}