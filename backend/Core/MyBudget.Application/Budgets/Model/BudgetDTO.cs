namespace MyBudget.Application.Budgets.Model;

public record BudgetDTO(
    Guid Id,
    string Name,
    string? Description,
    BudgetDTOStatus Status,
    DateTime CreationDate,
    IEnumerable<CategoryDTO> Categories
);

public enum BudgetDTOStatus
{
    Open,
    Closed
}