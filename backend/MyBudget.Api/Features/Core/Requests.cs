namespace MyBudget.Api.Features.Core;

public record CreateBudgetRequest(string Name);

public record CreateBudgetCategoryRequest(string Name);

public record CreateTransferRequest(string Name, decimal Value, string Currency, DateTime? Date = null);