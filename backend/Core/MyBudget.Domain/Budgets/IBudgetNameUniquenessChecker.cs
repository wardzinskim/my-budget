namespace MyBudget.Domain.Budgets;

public interface IBudgetNameUniquenessChecker
{
    Task<bool> IsUniqueAsync(Guid ownerId, string name, CancellationToken cancellationToken = default);
}