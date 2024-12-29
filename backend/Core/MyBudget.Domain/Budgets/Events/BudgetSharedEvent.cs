using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;

public record BudgetSharedEvent(BudgetId BudgetId, UserId UserId) : DomainEventBase
{
}