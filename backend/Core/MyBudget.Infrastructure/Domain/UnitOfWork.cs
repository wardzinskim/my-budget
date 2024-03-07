using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Domain;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BudgetContext _context;

    public UnitOfWork(BudgetContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
