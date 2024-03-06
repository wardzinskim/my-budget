namespace MyBudget.SharedKernel;

public abstract record DomainEventBase : IDomainEvent
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime OccurredOn { get; private set; } = DateTime.Now;
}