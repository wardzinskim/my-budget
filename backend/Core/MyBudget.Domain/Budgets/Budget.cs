using MyBudget.Domain.Budgets.Events;
using MyBudget.Domain.Budgets.Rules;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Domain.Budgets.Transfers.Events;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public class Budget : Entity, IAggregateRoot, IAuditable
{
    private Budget(Guid id, Guid ownerId, string name)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        Status = BudgetStatus.Open;
        _categories = [];
        _transfers = [];

        AddDomainEvent(new BudgetCreatedEvent(Id, OwnerId));
    }


    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime? LastUpdated { get; }
    public Guid OwnerId { get; init; }
    public BudgetStatus Status { get; private set; }

    private readonly List<TransferCategory> _categories;
    public IEnumerable<TransferCategory> Categories => _categories.AsEnumerable();

    private readonly List<Transfer> _transfers;

    public IEnumerable<Transfer> Transfers => _transfers.AsEnumerable();

    public static async Task<Result<Budget>> Create(
        IIdGenerator idGenerator,
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

        return new Budget(idGenerator.NextId(), ownerId, name);
    }


    public Result AddTransferCategory(string name)
    {
        var result = CheckRulesAsync(default, new TransferCategoryMustBeUniqueForBudget(name, _categories))
            .ConfigureAwait(false).GetAwaiter().GetResult();

        if (result.IsFailure)
        {
            return result.Error;
        }

        _categories.Add(TransferCategory.Create(name));
        AddDomainEvent(new BudgetCategoryCreatedEvent(Id, name));

        return Result.Success();
    }

    public Result ArchiveTransferCategory(string name)
    {
        TransferCategory? category = _categories.SingleOrDefault(x => x.Name == name);
        if (category is null)
        {
            return BudgetsErrors.BudgetCategoryNotFound;
        }

        category.Archive();
        AddDomainEvent(new BudgetCategoryArchivedEvent(Id, name));
        return Result.Success();
    }

    public Result HasAccess(Guid userId)
    {
        return OwnerId == userId ? Result.Success() : Result.Failure(BudgetsErrors.BudgetAccessDenied);
    }

    public Result<Transfer> AddTransfer(
        IIdGenerator idGenerator,
        TransferType type,
        TransferData data
    )
    {
        var transfer = Transfer.Create(idGenerator, Id, type, data.Name, data.Value, data.Currency, data.TransferDate);

        if (transfer.IsFailure) return transfer.Error;

        _transfers.Add(transfer.Value);

        return transfer.Value;
    }


    public Result DeleteTransfer(Guid transferId)
    {
        var result = CheckRulesAsync(default, new TransferMustExistsBeforeDeletion(_transfers, transferId))
            .ConfigureAwait(false).GetAwaiter().GetResult();

        if (result.IsFailure)
        {
            return result.Error;
        }

        var transfer = _transfers.First(x => x.Id == transferId);

        _transfers.Remove(transfer);

        AddDomainEvent(new TransferDeletedEvent(Id, transferId, transfer.Type, transfer.Value));
        return Result.Success();
    }

    public Result UpdateTransfer(Guid transferId, TransferData data)
    {
        var result = CheckRulesAsync(default, new TransferMustExistsBeforeDeletion(_transfers, transferId))
            .ConfigureAwait(false).GetAwaiter().GetResult();

        if (result.IsFailure)
        {
            return result.Error;
        }

        var transfer = _transfers.First(x => x.Id == transferId);

        var updateResult = transfer.Update(data);
        if (updateResult.IsFailure)
            return updateResult.Error;

        return Result.Success();
    }
}