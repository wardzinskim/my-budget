using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers.Events;

public record TransferDeletedEvent(Guid BudgetId, Guid TransferId, TransferType Type, Money Value) : DomainEventBase;