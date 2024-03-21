using MassTransit.Mediator;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.GetTransfers;

public record GetTransfersQuery(Guid BudgetId, DateTime? DateFrom, DateTime? DateTo) : Request<Result>;