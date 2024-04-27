using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.GetTransfer;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets.Transfers;

public class GetTransferQueryHandler(
    BudgetContext dbContext,
    IRequestContext context
) : MediatorRequestHandler<GetTransferQuery, Result<TransferDTO>>
{
    protected override async Task<Result<TransferDTO>> Handle(
        GetTransferQuery request,
        CancellationToken cancellationToken
    )
    {
        var budget = await dbContext.Budgets.AsNoTracking()
            .Where(x => x.Id == request.BudgetId)
            .Include(x => x.Transfers.Where(t =>
                t.Id == request.TransferId)
            )
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (budget is null)
            return BudgetsErrors.BudgetNotFound;

        var access = budget.HasAccess(context.UserId);
        if (access.IsFailure)
        {
            return access.Error;
        }

        var transfer = budget.Transfers.FirstOrDefault();
        if (transfer is null)
        {
            return BudgetsErrors.TransferNotFound;
        }

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