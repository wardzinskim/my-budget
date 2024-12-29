namespace MyBudget.Application.Budgets.Model;

public record BudgetDTO(
    Guid Id,
    string Name,
    string? Description,
    Guid OwnerId,
    BudgetDTOStatus Status,
    DateTime CreationDate,
    IEnumerable<CategoryDTO> Categories,
    IEnumerable<ShareDTO> Shares
);

public enum BudgetDTOStatus
{
    Open,
    Closed
}