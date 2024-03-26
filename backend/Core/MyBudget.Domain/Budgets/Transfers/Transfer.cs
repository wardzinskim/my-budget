using MyBudget.Domain.Budgets.Transfers.Events;
using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers;

public class Transfer : Entity, IAuditable
{
    private Transfer(
        Guid id,
        Guid budgetId,
        TransferType type,
        string name,
        Money value,
        DateTime transferDate,
        string? category
    )
    {
        Id = id;
        BudgetId = budgetId;
        Type = type;
        Value = value;
        TransferDate = transferDate;
        Name = name;
        Category = category;

        AddDomainEvent(new TransferCreatedEvent(BudgetId, Id));
    }


    private Transfer() { }

    public Guid Id { get; init; }
    public Guid BudgetId { get; init; }
    public DateTime TransferDate { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime? LastUpdated { get; }
    public Money Value { get; private set; }
    public TransferType Type { get; private set; }
    public string? Category { get; private set; }
    public string Name { get; private set; }

    internal Result Update(TransferData data)
    {
        var money = Money.Of(data.Value, data.Currency);
        if (money.IsFailure) return money.Error;

        TransferDate = data.TransferDate;
        Name = data.Name;
        Value = money.Value;
        Category = data.Category;

        return Result.Success();
    }

    internal static Result<Transfer> Create(
        IIdGenerator idGenerator,
        Guid budgetId,
        TransferType type,
        string name,
        decimal value,
        string currency,
        DateTime transferDate,
        string? category
    )
    {
        var money = Money.Of(value, currency);
        if (money.IsFailure) return money.Error;

        return new Transfer(idGenerator.NextId(), budgetId, type, name, money.Value, transferDate, category);
    }
}