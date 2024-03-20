namespace MyBudget.Application.Budgets.Model;

public record CategoryDTO(string Name, CategoryDTOStatus Status);

public enum CategoryDTOStatus
{
    Active,
    Archived,
}