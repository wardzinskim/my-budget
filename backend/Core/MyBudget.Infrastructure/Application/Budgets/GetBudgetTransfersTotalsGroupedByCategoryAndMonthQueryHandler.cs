using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategoryAndMonth;
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets;

public class GetBudgetTransfersTotalsGroupedByCategoryAndMonthQueryHandler(
    IBudgetAccessValidator budgetAccessValidator,
    BudgetContext context
)
    : MediatorRequestHandler<GetBudgetTransfersTotalsGroupedByCategoryAndMonthQuery, Result<CategoryMonthValue[]>>
{
    protected override async Task<Result<CategoryMonthValue[]>> Handle(
        GetBudgetTransfersTotalsGroupedByCategoryAndMonthQuery request,
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

        var results = await context.Transfers
            .AsNoTracking()
            .Where(x => x.BudgetId == request.Id)
            .Where(x => x.Type == type)
            .Where(x => x.TransferDate.Year == request.Year)
            .GroupBy(x => new { x.TransferDate.Month, x.Category })
            .Select(x => new CategoryMonthValue(x.Key.Month, x.Key.Category, x.Sum(t => t.Value.Value)))
            .ToArrayAsync(cancellationToken).ConfigureAwait(false);

        return results;
    }
}

