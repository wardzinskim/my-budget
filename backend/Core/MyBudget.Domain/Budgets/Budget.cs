using MyBudget.Domain.Budgets.Events;
using MyBudget.Domain.Budgets.Rules;
using MyBudget.Domain.Budgets.Transfers;
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
    public IEnumerable<Transfer> Expenses => _transfers.Where(x => x.Type == TransferType.Expense).AsEnumerable();
    public IEnumerable<Transfer> Incomes => _transfers.Where(x => x.Type == TransferType.Income).AsEnumerable();


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

    public Result<Transfer> AddExpense(
        IIdGenerator idGenerator,
        string name,
        decimal value,
        string currency,
        DateTime expenseDate
    )
    {
        var transfer = Transfer.Create(idGenerator, Id, TransferType.Expense, name, value, currency, expenseDate);

        if (transfer.IsFailure) return transfer.Error;

        _transfers.Add(transfer.Value);

        return transfer.Value;
    }
}