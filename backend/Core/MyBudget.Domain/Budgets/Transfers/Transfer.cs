using MyBudget.Domain.Budgets.Transfers.Events;
using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers;

public class Transfer : Entity, IAggregateRoot, IAuditable
{
    private Transfer(
        TransferId id,
        BudgetId budgetId,
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
        Status = TransferStatus.Active;

        AddDomainEvent(new TransferCreatedEvent(BudgetId, Id));
    }


    private Transfer() { }

    public TransferId Id { get; init; }
    public BudgetId BudgetId { get; init; }
    public DateTime TransferDate { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime? LastUpdated { get; }
    public Money Value { get; private set; }
    public TransferType Type { get; private set; }
    public string? Category { get; private set; }
    public string Name { get; private set; }
    public TransferStatus Status { get; private set; }

    public Result Update(TransferData data)
    {
        var money = Money.Of(data.Value, data.Currency);
        if (money.IsFailure) return money.Error;

        TransferDate = data.TransferDate;
        Name = data.Name;
        var prevValue = Value;
        Value = money.Value;
        Category = data.Category;

        AddDomainEvent(new TransferUpdatedEvent(BudgetId, Id, Type, prevValue, money.Value));

        return Result.Success();
    }

    internal static Result<Transfer> Create(
        IIdGenerator idGenerator,
        BudgetId budgetId,
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

        var transferId = TransferId.Of(idGenerator.NextId());
        if (transferId.IsFailure) return transferId.Error;

        return new Transfer(transferId.Value, budgetId, type, name, money.Value, transferDate, category);
    }

    public Result Delete()
    {
        Status = TransferStatus.Deleted;
        AddDomainEvent(new TransferDeletedEvent(BudgetId, Id, Type, Value));
        return Result.Success();
    }
}