using System.ComponentModel;

namespace MyBudget.Application.Budgets.Model;

public record CategoryDTO(
    [property: Description("The unique name of the category")]
    string Name,
    [property: Description("The status of the category")]
    CategoryDTOStatus Status
);

public enum CategoryDTOStatus
{
    Active,
    Archived,
}