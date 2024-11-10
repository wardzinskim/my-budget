using MyBudget.Domain.Budgets;

namespace MyBudget.Application.Budgets.Services;

public interface IBudgetAccessValidator
{
    public Result HasUserAccess(Budget budget);
}