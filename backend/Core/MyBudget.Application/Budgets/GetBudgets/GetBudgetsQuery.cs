using MyBudget.Application.Budgets.Model;

namespace MyBudget.Application.Budgets.GetBudgets;

public record GetBudgetsQuery : Request<Result<IEnumerable<BudgetDTO>>>;