using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetsQueryHandler(BudgetContext context, IRequestContext requestContext)
    : MediatorRequestHandler<GetBudgetsQuery, Result<IEnumerable<BudgetListItemDTO>>>
{
    protected override async Task<Result<IEnumerable<BudgetListItemDTO>>> Handle(
        GetBudgetsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Budgets
            .Where(x => x.OwnerId == requestContext.UserId)
            .AsNoTracking()
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Description,
                x.Status,
                x.Categories,
                x.CreationDate
            })
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return result
            .Select(x => new BudgetListItemDTO(x.Id,
                    x.Name,
                    x.Description,
                    x.CreationDate,
                    (BudgetDTOStatus)x.Status
                )
            ).ToList();
    }
}