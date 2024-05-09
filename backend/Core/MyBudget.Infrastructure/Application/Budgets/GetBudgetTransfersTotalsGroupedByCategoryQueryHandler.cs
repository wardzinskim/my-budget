using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgetTransfersTotalsGroupedByCategory;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets;
public class GetBudgetTransfersTotalsGroupedByCategoryQueryHandler(IRequestContext requestContext, BudgetContext context)
    : MediatorRequestHandler<GetBudgetTransfersTotalsGroupedByCategoryQuery, Result<CategoryValue[]>>
{
    private readonly IRequestContext _requestContext = requestContext;
    private readonly BudgetContext _context = context;


    protected override async Task<Result<CategoryValue[]>> Handle(GetBudgetTransfersTotalsGroupedByCategoryQuery request, CancellationToken cancellationToken)
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

        TransferType type = (TransferType)request.Type;

        var budgetTransfersQuery = _context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SelectMany(x => x.Transfers)
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

        var results = await budgetTransfersQuery
           .GroupBy(x => x.Category)
           .Select(x => new CategoryValue(x.Key, x.Sum(x => x.Value.Value)))
           .ToArrayAsync(cancellationToken).ConfigureAwait(false);

        return results;
    }
}
