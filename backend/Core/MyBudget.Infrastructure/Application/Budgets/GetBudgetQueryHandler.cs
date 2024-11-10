using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Budgets.GetBudget;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Application.Budgets;

public sealed class GetBudgetQueryHandler(
    BudgetContext context,
    IBudgetAccessValidator budgetAccessValidator
)
    : MediatorRequestHandler<GetBudgetQuery, Result<BudgetDTO>>
{
    protected override async Task<Result<BudgetDTO>> Handle(GetBudgetQuery request, CancellationToken cancellationToken)
    {
        var budget = await context.Budgets
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        return new BudgetDTO(budget.Id,
            budget.Name,
            budget.Description,
            (BudgetDTOStatus)budget.Status,
            budget.CreationDate,
            budget.Categories.Select(x => new CategoryDTO(x.Name, (CategoryDTOStatus)x.Status)));
    }
}