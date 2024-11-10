using MyBudget.Domain.Budgets.Events;
using MyBudget.Domain.Budgets.Rules;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Domain.Budgets.Transfers.Events;
using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public class Budget : Entity, IAggregateRoot, IAuditable
{
    private Budget(BudgetId id, UserId ownerId, string name, string? description = null)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        Status = BudgetStatus.Open;
        Description = description;
        _categories = [];

        AddDomainEvent(new BudgetCreatedEvent(Id, OwnerId));
    }


    public BudgetId Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; private set; }
    public DateTime CreationDate { get; }
    public DateTime? LastUpdated { get; }
    public UserId OwnerId { get; init; }
    public BudgetStatus Status { get; private set; }

    private readonly List<TransferCategory> _categories;
    public IEnumerable<TransferCategory> Categories => _categories.AsEnumerable();

    public static async Task<Result<Budget>> Create(
        IIdGenerator idGenerator,
        IBudgetNameUniquenessChecker budgetNameUniquenessChecker,
        Guid ownerId,
        string name,
        string? description,
        CancellationToken cancellationToken = default
    )
    {
        var userId = UserId.Of(ownerId);
        if (userId.IsFailure) return userId.Error;

        var result = await CheckRulesAsync(
            cancellationToken,
            new BudgetNameMustBeUniqueForUser(name, userId.Value, budgetNameUniquenessChecker)
        ).ConfigureAwait(false);

        if (result.IsFailure) return result.Error;
        var budgetId = BudgetId.Of(idGenerator.NextId());
        if (budgetId.IsFailure) return result.Error;

        return new Budget(budgetId.Value, userId.Value, name, description);
    }


    public Result AddTransferCategory(string name)
    {
        var result = CheckRules(
            new TransferCategoryMustBeUniqueForBudget(name, _categories));

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


    public Result<Transfer> AddTransfer(
        IIdGenerator idGenerator,
        TransferType type,
        TransferData data
    )
    {
        var result = CheckRules(new TransferCategoryMustBeDefined(_categories, data.Category));

        if (result.IsFailure) return result.Error;

        var transfer = Transfer.Create(
            idGenerator,
            Id,
            type,
            data.Name,
            data.Value,
            data.Currency,
            data.TransferDate,
            data.Category);

        if (transfer.IsFailure) return transfer.Error;

        return transfer.Value;
    }
}