namespace MyBudget.Domain.Budgets.Transfers;

public interface ITransferRepository
{
    Task AddAsync(Transfer budget, CancellationToken cancellationToken = default);
    Task<Transfer?> GetAsync(Guid budgetId, Guid transferId, CancellationToken cancellationToken = default);
}