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
    private readonly IRequestContext _requestContext = requestContext;
    private readonly BudgetContext _context = context;

    protected override async Task<Result<IEnumerable<BudgetDTO>>> Handle(
        GetBudgetsQuery request,
        CancellationToken cancellationToken
    )
        => await _context.Budgets.Where(x => x.OwnerId == _requestContext.UserId)
            .Select(x => new BudgetDTO(x.Id, x.Name, x.Description))
            .ToListAsync(cancellationToken).ConfigureAwait(false);
}