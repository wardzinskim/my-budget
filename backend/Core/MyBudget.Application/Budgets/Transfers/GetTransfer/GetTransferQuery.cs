using MassTransit.Mediator;
using MyBudget.Application.Budgets.Model;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.GetTransfer;

public record GetTransferQuery(Guid BudgetId, Guid TransferId) : Request<Result<TransferDTO>>;