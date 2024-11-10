using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;

public record BudgetCategoryCreatedEvent(BudgetId BudgetId, string Name) : DomainEventBase
{
}