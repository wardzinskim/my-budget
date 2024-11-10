using MyBudget.Application.Budgets.Model;

namespace MyBudget.Application.Budgets.Transfers.GetTransfer;

public record GetTransferQuery(Guid BudgetId, Guid TransferId) : Request<Result<TransferDTO>>;