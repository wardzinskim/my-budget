using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers.Events;

public record TransferDeletedEvent(BudgetId BudgetId, TransferId TransferId, TransferType Type, Money Value)
    : DomainEventBase;