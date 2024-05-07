using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgetTotals;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetTotalsQueryHandler(IRequestContext requestContext, BudgetContext context)
    : MediatorRequestHandler<GetBudgetTotalsQuery, Result<BudgetTotals>>
{
    private readonly IRequestContext _requestContext = requestContext;
    private readonly BudgetContext _context = context;

    protected override async Task<Result<BudgetTotals>> Handle(
        GetBudgetTotalsQuery request,
        CancellationToken cancellationToken
    )
    {
        var budget = await _context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        var access = budget.HasAccess(_requestContext.UserId);
        if (access.IsFailure)
        {
            return access.Error;
        }


        var budgetTransfersQuery = _context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SelectMany(x => x.Transfers);

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

        var totals = await budgetTransfersQuery
            .GroupBy(x => x.Type)
            .Select(x => new {Type = x.Key, TotalValue = x.Sum(x => x.Value.Value)})
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new BudgetTotals(
            totals.FirstOrDefault(x => x.Type == TransferType.Income)?.TotalValue ??
            0,
            totals.FirstOrDefault(x => x.Type == TransferType.Expense)?.TotalValue ??
            0
        );
    }
}