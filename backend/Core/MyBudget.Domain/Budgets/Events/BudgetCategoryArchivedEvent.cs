using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Events;

public record BudgetCategoryArchivedEvent(BudgetId BudgetId, string Name) : DomainEventBase
{
}