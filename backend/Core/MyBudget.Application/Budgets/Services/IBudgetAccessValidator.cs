using MyBudget.Domain.Budgets;

namespace MyBudget.Application.Budgets.Services;

public interface IBudgetAccessValidator
{
    public Result IsOwner(Budget budget);

    public Result HasUserAccess(Budget budget);
}

public class BudgetAccessValidator(IRequestContext requestContext) : IBudgetAccessValidator
{
    public Result IsOwner(Budget budget)
        => budget.OwnerId != requestContext.UserId ? BudgetsErrors.BudgetAccessDenied : Result.Success();

    public Result HasUserAccess(Budget budget)
        => budget.OwnerId == requestContext.UserId || budget.Shares.Any(x => x.UserId == requestContext.UserId)
            ? Result.Success()
            : BudgetsErrors.BudgetAccessDenied;
}