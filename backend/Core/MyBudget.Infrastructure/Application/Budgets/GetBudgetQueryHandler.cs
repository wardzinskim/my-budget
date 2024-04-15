using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudget;
using MyBudget.Application.Budgets.Model;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetQueryHandler(IRequestContext requestContext, BudgetContext context)
    : MediatorRequestHandler<GetBudgetQuery, Result<BudgetDTO>>
{
    private readonly IRequestContext _requestContext = requestContext;
    private readonly BudgetContext _context = context;

    protected override async Task<Result<BudgetDTO>> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
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

        return new BudgetDTO(budget.Id,
            budget.Name,
            budget.Description,
            (BudgetDTOStatus)budget.Status,
            budget.Categories.Select(x => new CategoryDTO(x.Name, (CategoryDTOStatus)x.Status)));
    }
}