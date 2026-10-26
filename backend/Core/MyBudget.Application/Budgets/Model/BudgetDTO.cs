using System.ComponentModel;

namespace MyBudget.Application.Budgets.Model;

public record BudgetDTO(
    [property: Description("The unique identifier of the budget")]
    Guid Id,
    [property: Description("The name of the budget")]
    string Name,
    [property: Description("The description of the budget")]
    string? Description,
    [property: Description("The owner of the budget")]
    Guid OwnerId,
    [property: Description("The status of the budget")]
    BudgetDTOStatus Status,
    DateTime CreationDate,
    [property: Description("Categories of the budget. It can be assigned to transfer")]
    IEnumerable<CategoryDTO> Categories,
    IEnumerable<ShareDTO> Shares
);

public enum BudgetDTOStatus
{
    Open,
    Closed
}