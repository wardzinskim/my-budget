using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budget.Events;

public record BudgetCreatedEvent(Guid BudgetId, Guid UserId) : DomainEventBase
{
}