using MyBudget.Domain.Budgets;

namespace MyBudget.Application.Budgets.Services;

public interface IBudgetAccessValidator
{
    public Result HasUserAccess(Budget budget);
}

public class BudgetAccessValidator(IRequestContext requestContext) : IBudgetAccessValidator
{
    public Result HasUserAccess(Budget budget)
        => budget.OwnerId != requestContext.UserId ? BudgetsErrors.BudgetAccessDenied : Result.Success();
}