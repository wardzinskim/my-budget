using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Services;
using MyBudget.Application.Budgets.Transfers.GetTransfer;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets.Transfers;

public class GetTransferQueryHandler(
    BudgetContext dbContext,
    IBudgetAccessValidator budgetAccessValidator
) : MediatorRequestHandler<GetTransferQuery, Result<TransferDTO>>
{
    protected override async Task<Result<TransferDTO>> Handle(
        GetTransferQuery request,
        CancellationToken cancellationToken
    )
    {
        var budget = await dbContext.Budgets.AsNoTracking()
            .Where(x => x.Id == request.BudgetId)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var transfer = await dbContext.Transfers
            .Where(x => x.BudgetId == request.BudgetId && x.Id == request.TransferId)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        if (transfer is null) return BudgetsErrors.TransferNotFound;

        return new TransferDTO(
            transfer.Id,
            transfer.TransferDate,
            transfer.Value.Value,
            transfer.Value.Currency,
            (TransferDTOType)transfer.Type,
            transfer.Name,
            transfer.Category
        );
    }
}