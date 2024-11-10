using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgetTotals;
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetTotalsQueryHandler(IBudgetAccessValidator budgetAccessValidator, BudgetContext context)
    : MediatorRequestHandler<GetBudgetTotalsQuery, Result<BudgetTotals>>
{
    protected override async Task<Result<BudgetTotals>> Handle(
        GetBudgetTotalsQuery request,
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

        IQueryable<Transfer> transfers = BuildTransfersQuery(request);

        var totals = await transfers
            .GroupBy(x => x.Type)
            .Select(x => new {Type = x.Key, TotalValue = x.Sum(v => v.Value.Value)})
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new BudgetTotals(
            totals.FirstOrDefault(x => x.Type == TransferType.Income)?.TotalValue ??
            0,
            totals.FirstOrDefault(x => x.Type == TransferType.Expense)?.TotalValue ??
            0
        );
    }

    private IQueryable<Transfer> BuildTransfersQuery(GetBudgetTotalsQuery request)
    {
        var transfers = context.Transfers
            .AsNoTracking()
            .Where(x => x.BudgetId == request.Id);

        if (request.Year.HasValue)
        {
            transfers = transfers
                .Where(x => x.TransferDate.Year == request.Year);
        }

        if (request.Month.HasValue)
        {
            transfers = transfers
                .Where(x => x.TransferDate.Month == request.Month);
        }

        return transfers;
    }
}