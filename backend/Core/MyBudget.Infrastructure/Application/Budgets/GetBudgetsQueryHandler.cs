using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetsQueryHandler(BudgetContext context, IRequestContext requestContext)
    : MediatorRequestHandler<GetBudgetsQuery, Result<IEnumerable<BudgetDTO>>>
{
    protected override async Task<Result<IEnumerable<BudgetDTO>>> Handle(
        GetBudgetsQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await context.Budgets
            .Where(x => x.OwnerId == requestContext.UserId)
            .AsNoTracking()
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return result
            .Select(x => new BudgetDTO(x.Id,
                    x.Name,
                    x.Description,
                    (BudgetDTOStatus)x.Status,
                    x.CreationDate,
                    x.Categories.Select(y => new CategoryDTO(y.Name, (CategoryDTOStatus)y.Status))
                )
            ).ToList();
    }
}