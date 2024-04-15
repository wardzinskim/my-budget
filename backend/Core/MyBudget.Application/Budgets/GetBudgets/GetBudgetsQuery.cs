using MassTransit.Mediator;
using MyBudget.Application.Budgets.Model;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.GetBudgets;

public record GetBudgetsQuery : Request<Result<IEnumerable<BudgetListItemDTO>>>;