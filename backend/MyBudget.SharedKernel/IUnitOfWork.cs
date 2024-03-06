namespace MyBudget.SharedKernel;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}