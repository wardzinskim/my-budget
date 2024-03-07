using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Domain.Budgets;
internal class BudgetRepository(BudgetContext context) : IBudgetRepository
{
    private readonly BudgetContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        => await _context.Budgets.AddAsync(budget, cancellationToken);
}
