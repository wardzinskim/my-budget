using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Services;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets.Transfers;

public sealed class GetTransfersQueryHandler(
    BudgetContext dbContext,
    IBudgetAccessValidator budgetAccessValidator,
    IDateTimeProvider timeProvider
) : MediatorRequestHandler<GetTransfersQuery, Result<TransfersQueryResponse>>
{
    private const int DEFAULT_DATE_SPAN = 30;

    protected override async Task<Result<TransfersQueryResponse>> Handle(
        GetTransfersQuery request,
        CancellationToken cancellationToken
    )
    {
        var dateTo = request.DateTo ?? timeProvider.UtcNow.Date.AddDays(1);
        var dateFrom = request.DateFrom ?? dateTo.AddDays(-DEFAULT_DATE_SPAN);
        var type = (TransferType?)request.Type;

        var budget = await dbContext.Budgets.AsNoTracking()
            .Where(x => x.Id == request.BudgetId)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var transfers = await dbContext.Transfers
            .Where(x => x.BudgetId == request.BudgetId && (type == null || x.Type == type))
            .Where(x => x.TransferDate >= dateFrom && x.TransferDate <= dateTo)
            .OrderByDescending(x => x.TransferDate)
            .Select(x => new
            {
                x.Id,
                x.TransferDate,
                x.Value.Value,
                x.Value.Currency,
                x.Type,
                x.Name,
                x.Category
            }).ToArrayAsync(cancellationToken);

        return new TransfersQueryResponse(
            dateFrom,
            dateTo,
            transfers.Select(
                x => new TransferDTO(
                    x.Id,
                    x.TransferDate,
                    x.Value,
                    x.Currency,
                    (TransferDTOType)x.Type,
                    x.Name,
                    x.Category))
        );
    }
}