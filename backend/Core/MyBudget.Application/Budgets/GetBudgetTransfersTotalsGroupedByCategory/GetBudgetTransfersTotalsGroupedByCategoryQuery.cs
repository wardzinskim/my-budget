using MassTransit.Mediator;
using MyBudget.Application.Budgets.Model;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
public record GetBudgetTransfersTotalsGroupedByCategoryQuery(Guid Id, TransferDTOType Type, int? Year, int? Month)
    : Request<Result<CategoryValue[]>>;

public record CategoryValue(string? Category, decimal Value);