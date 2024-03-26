using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets.Transfers;

public sealed class GetTransfersQueryHandler(
    BudgetContext dbContext,
    IRequestContext context,
    IDateTimeProvider timeProvider
) : MediatorRequestHandler<GetTransfersQuery, Result<TransfersQueryResponse>>
{
    private const int DEFAULT_DATE_SPAN = 30;

    protected override async Task<Result<TransfersQueryResponse>> Handle(
        GetTransfersQuery request,
        CancellationToken cancellationToken
    )
    {
        var dateTo = request.DateTo ?? timeProvider.UtcNow.Date;
        var dateFrom = request.DateFrom ?? dateTo.AddDays(-DEFAULT_DATE_SPAN);
        var type = (TransferType?)request.Type;

        var budget = await dbContext.Budgets.AsNoTracking()
            .Where(x => x.Id == request.BudgetId)
            .Include(x => x.Transfers.Where(t =>
                t.TransferDate >= dateFrom && t.TransferDate <= dateTo && (type == null || t.Type == type))
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

        return new TransfersQueryResponse(dateFrom, dateTo,
            budget.Transfers.Select(x => new TransferDTO(
                x.Id,
                x.TransferDate,
                x.Value.Value,
                x.Value.Currency,
                (TransferDTOType)x.Type,
                x.Name,
                x.Category)));
    }
}