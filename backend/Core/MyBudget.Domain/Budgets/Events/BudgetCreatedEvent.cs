using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;

public record BudgetCreatedEvent(Guid BudgetId, Guid UserId) : DomainEventBase
{
}