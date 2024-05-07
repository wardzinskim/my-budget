using MassTransit.Mediator;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.GetBudgetTotals;


public record GetBudgetTotalsQuery(Guid Id, int? Year = null, int? Month = null) 
    : Request<Result<BudgetTotals>>;

public record BudgetTotals(decimal Incomes, decimal Expenses);
