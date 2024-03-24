using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;
using System.Linq.Expressions;

namespace MyBudget.Infrastructure.Domain.Budgets;

internal class BudgetRepository(BudgetContext context) : IBudgetRepository
{
    private readonly BudgetContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddAsync(Budget budget, CancellationToken cancellationToken = default)
        => await _context.Budgets.AddAsync(budget, cancellationToken);

    public async Task<Budget?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Budgets
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<Budget?> GetAsync(
        Guid id,
        Expression<Func<Transfer, bool>> includeTransfersFilter,
        CancellationToken cancellationToken = default
    )
        => await _context.Budgets
            .Include(x => x.Transfers.AsQueryable().Where(includeTransfersFilter))
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
}