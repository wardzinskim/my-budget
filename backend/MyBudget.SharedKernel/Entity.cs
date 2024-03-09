namespace MyBudget.SharedKernel;

public abstract class Entity
{
    private List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
        => _domainEvents.Clear();


    protected static async ValueTask<Result> CheckRulesAsync(
        CancellationToken cancellationToken = default,
        params IBusinessRule[] rules)
    {
        foreach (var rule in rules)
        {
            var result = await rule.ValidateAsync(cancellationToken).ConfigureAwait(false);
            if (result.IsFailure) return result;
        }

        return Result.Success();
    }
}