using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Domain.Budgets.Transfers;

public class TransferRepository(BudgetContext context) : ITransferRepository
{
    private readonly BudgetContext _context = context ?? throw new ArgumentNullException(nameof(context));


    public async Task AddAsync(Transfer budget, CancellationToken cancellationToken = default)
        => await _context.Transfers.AddAsync(budget, cancellationToken);

    public async Task<Transfer?> GetAsync(
        Guid budgetId,
        Guid transferId,
        CancellationToken cancellationToken = default
    )
        => await _context.Transfers
            .Where(x => x.Id == transferId && x.BudgetId == budgetId)
            .SingleOrDefaultAsync(cancellationToken);
}