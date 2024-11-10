using MyBudget.Application.Budgets.Model;

namespace MyBudget.Application.Budgets.Transfers.GetTransfers;

public record GetTransfersQuery(Guid BudgetId, TransferDTOType? Type, DateTime? DateFrom, DateTime? DateTo)
    : Request<Result<TransfersQueryResponse>>;

public record TransfersQueryResponse(DateTime DateFrom, DateTime DateTo, IEnumerable<TransferDTO> Transfers);