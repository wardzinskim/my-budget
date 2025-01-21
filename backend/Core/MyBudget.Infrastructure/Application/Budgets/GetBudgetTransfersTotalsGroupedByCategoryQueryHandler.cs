using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets;

public class GetBudgetTransfersTotalsGroupedByCategoryQueryHandler(
    IBudgetAccessValidator budgetAccessValidator,
    BudgetContext context
)
    : MediatorRequestHandler<GetBudgetTransfersTotalsGroupedByCategoryQuery, Result<CategoryValue[]>>
{
    protected override async Task<Result<CategoryValue[]>> Handle(
        GetBudgetTransfersTotalsGroupedByCategoryQuery request,
        CancellationToken cancellationToken
    )
    {
        var budget = await context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        TransferType type = (TransferType)request.Type;

        IQueryable<Transfer> budgetTransfersQuery = BuildTransfersQuery(request, type);

        var results = await budgetTransfersQuery
            .GroupBy(x => x.Category)
            .Select(x => new CategoryValue(x.Key, x.Sum(t => t.Value.Value)))
            .ToArrayAsync(cancellationToken).ConfigureAwait(false);

        return results;
    }

    private IQueryable<Transfer> BuildTransfersQuery(
        GetBudgetTransfersTotalsGroupedByCategoryQuery request,
        TransferType type
    )
    {
        var budgetTransfersQuery = context.Transfers
            .AsNoTracking()
            .Where(x => x.BudgetId == request.Id)
            .Where(x => x.Type == type);

        if (request.Year.HasValue)
        {
            budgetTransfersQuery = budgetTransfersQuery
                .Where(x => x.TransferDate.Year == request.Year);
        }

        if (request.Month.HasValue)
        {
            budgetTransfersQuery = budgetTransfersQuery
                .Where(x => x.TransferDate.Month == request.Month);
        }

        return budgetTransfersQuery;
    }
}