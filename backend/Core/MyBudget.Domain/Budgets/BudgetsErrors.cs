using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public static class BudgetsErrors
{
    public static readonly Error BudgetNotFound =
        new NotFoundError(nameof(BudgetNotFound), "Budget with the specified id not exists.");

    public static readonly Error BudgetCategoryNotFound = new NotFoundError(nameof(BudgetCategoryNotFound),
        "Budget category with the specified name not exists.");

    public static readonly Error BudgetAccessDenied =
        new ForbiddenError(nameof(BudgetAccessDenied), "You do not have access to specified budget.");

    public static readonly Error TransferNotFound =
        new NotFoundError(nameof(TransferNotFound), "Transfer with this id not exists.");
}