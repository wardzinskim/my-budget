namespace MyBudget.Application.Budgets.Model;
public record BudgetDTO(Guid Id, string Name, string? Description, BudgetDTOStatus Status);
