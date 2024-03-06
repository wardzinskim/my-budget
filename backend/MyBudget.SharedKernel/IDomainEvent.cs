namespace MyBudget.SharedKernel;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}