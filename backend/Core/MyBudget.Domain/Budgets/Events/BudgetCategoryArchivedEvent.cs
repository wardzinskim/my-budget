using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;

public record BudgetCategoryArchivedEvent(Guid BudgetId, string Name) : DomainEventBase
{
}