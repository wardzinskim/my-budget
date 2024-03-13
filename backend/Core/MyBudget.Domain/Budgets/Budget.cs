using MyBudget.Domain.Budgets.Events;
using MyBudget.Domain.Budgets.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public class Budget : Entity, IAggregateRoot
{
    protected internal Budget(Guid id, Guid ownerId, DateTime creationDate, string name)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        Status = BudgetStatus.Open;
        CreationDate = creationDate;

        AddDomainEvent(new BudgetCreatedEvent(Id, OwnerId));
    }


    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; private set; }
    public DateTime CreationDate { get; init; }
    public Guid OwnerId { get; init; }
    public BudgetStatus Status { get; private set; }


    public static async Task<Result<Budget>> Create(
        IIdGenerator idGenerator,
        IDateTimeProvider dateTimeProvider,
        IBudgetNameUniquenessChecker budgetNameUniquenessChecker,
        Guid ownerId,
        string name,
        CancellationToken cancellationToken = default
    )
    {
        var result = await CheckRulesAsync(
            cancellationToken,
            new BudgetNameMustBeUniqueForUser(name, ownerId, budgetNameUniquenessChecker)
        ).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new Budget(idGenerator.NextId(), ownerId, dateTimeProvider.UtcNow, name);
    }
}