namespace MyBudget.Domain.Budgets;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget, CancellationToken cancellationToken = default);
}