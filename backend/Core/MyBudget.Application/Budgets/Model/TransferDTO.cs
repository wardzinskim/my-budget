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
    Income,
    Expense
}