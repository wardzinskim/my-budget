namespace MyBudget.Domain.Budgets.Transfers;

public record TransferData(string Name, decimal Value, string Currency, DateTime TransferDate, string? Category = null);