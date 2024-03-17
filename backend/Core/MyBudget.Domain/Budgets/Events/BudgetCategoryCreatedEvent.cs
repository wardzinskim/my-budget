using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;
public record BudgetCategoryCreatedEvent(Guid BudgetId, string Name) : DomainEventBase
{
}
