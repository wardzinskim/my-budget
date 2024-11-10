using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers.Events;

public record TransferCreatedEvent(BudgetId BudgetId, TransferId TransferId) : DomainEventBase;