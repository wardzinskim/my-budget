using MyBudget.Domain.Shared;

namespace MyBudget.Domain.Budgets;

public interface IBudgetNameUniquenessChecker
{
    Task<bool> IsUniqueAsync(UserId ownerId, string name, CancellationToken cancellationToken = default);
}