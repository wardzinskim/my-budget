using MyBudget.Domain.Budgets.Transfers;
using System.Linq.Expressions;

namespace MyBudget.Domain.Budgets;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget, CancellationToken cancellationToken = default);
    Task<Budget?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Budget?> GetAsync(
        Guid id,
        Expression<Func<Transfer, bool>> includeTransfersFilter,
        CancellationToken cancellationToken = default
    );
}