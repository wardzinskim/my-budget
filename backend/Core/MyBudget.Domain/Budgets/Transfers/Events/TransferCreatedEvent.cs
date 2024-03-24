using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers.Events;

public record TransferCreatedEvent(Guid BudgetId, Guid TransferId) : DomainEventBase;