namespace MyBudget.Domain.Budgets;

public record TransferCategory
{
    private TransferCategory(string name)
    {
        Name = name;
        Status = TransferCategoryStatus.Active;
    }

    public string Name { get; init; }
    public TransferCategoryStatus Status { get; init; }

    internal static TransferCategory Create(string name)
    {
        return new TransferCategory(name);
    }
}