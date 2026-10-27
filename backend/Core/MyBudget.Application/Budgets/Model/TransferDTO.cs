using System.ComponentModel;

namespace MyBudget.Application.Budgets.Model;

public record TransferDTO(
    Guid Id,
    DateTime TransferDate,
    decimal Value,
    string Currency,
    TransferDTOType Type,
    string Name,
    string? Category
);

public enum TransferDTOType
{
    [Description("Money received, increasing the budget balance")]
    Income,

    [Description("Money spent, decreasing the budget balance")]
    Expense,

    [Description("Tax-related transfer, e.g.  VAT, PIT or ZUS payments")]
    Tax,

    [Description("Money set aside as savings. Tracked separately and does not affect incomes, expenses or taxes")]
    Savings
}