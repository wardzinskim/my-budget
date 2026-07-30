using MyBudget.Application.Budgets.Model;

namespace MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategoryAndMonth;

public record GetBudgetTransfersTotalsGroupedByCategoryAndMonthQuery(Guid Id, TransferDTOType Type, int Year)
    : Request<Result<CategoryMonthValue[]>>;

public record CategoryMonthValue(int Month, string? Category, decimal Value);

