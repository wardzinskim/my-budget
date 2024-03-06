using MyBudget.Domain.Budget.Events;
using MyBudget.Domain.Budget.Rules;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budget;

public class Budget : Entity, IAggregateRoot
{
    private Budget(Guid id, Guid ownerId, DateTime creationDate, string name)
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


    public static Result<Budget> Create(
        IIdGenerator idGenerator,
        IDateTimeProvider dateTimeProvider,
        Guid ownerId,
        string name
    )
    {
        var result = CheckRules(
            new BudgetNameMustBeUniqueForUser(name, ownerId)
        );

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new Budget(idGenerator.NextId(), ownerId, dateTimeProvider.UtcNow, name);
    }
}