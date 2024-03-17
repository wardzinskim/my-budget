namespace MyBudget.Domain.Budgets;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget, CancellationToken cancellationToken = default);
    Task<Budget?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}