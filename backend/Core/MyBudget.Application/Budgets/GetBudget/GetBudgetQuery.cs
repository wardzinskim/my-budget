using MassTransit.Mediator;
using MyBudget.Application.Budgets.Model;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.GetBudget;
public record GetBudgetQuery(Guid Id) : Request<Result<BudgetDTO>>;
