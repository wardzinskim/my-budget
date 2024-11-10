using MyBudget.Application.Budgets.Model;

namespace MyBudget.Application.Budgets.GetBudget;
public record GetBudgetQuery(Guid Id) : Request<Result<BudgetDTO>>;
