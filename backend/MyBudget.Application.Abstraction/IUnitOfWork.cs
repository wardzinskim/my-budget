namespace MyBudget.Application.Abstraction;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}