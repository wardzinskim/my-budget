using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Domain.Budgets;

internal class BudgetNameUniquenessChecker(BudgetContext context) : IBudgetNameUniquenessChecker
{
    private readonly BudgetContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public Task<bool> IsUniqueAsync(Guid ownerId, string name, CancellationToken cancellationToken = default)
        => _context.Budgets.AnyAsync(x => x.OwnerId == ownerId && x.Name == name, cancellationToken);
}