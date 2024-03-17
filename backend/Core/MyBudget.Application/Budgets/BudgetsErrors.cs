using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets;

public static class BudgetsErrors
{
    public static Error BudgetNotFound =
        new NotFoundError(nameof(BudgetNotFound), "Budget with the specified id not exists.");
}