namespace MyBudget.Application.Budgets.Model;

public record TransferDTO(
    Guid Id,
    DateTime TransferDate,
    decimal Value,
    string Currency,
    TransferDTOType Type,
    string Name
);

public enum TransferDTOType
{
    Income,
    Expense
}