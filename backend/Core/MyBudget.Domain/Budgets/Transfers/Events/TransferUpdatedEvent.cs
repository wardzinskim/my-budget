using MyBudget.Domain.Shared;
using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets.Transfers.Events;

public record TransferUpdatedEvent(
    BudgetId BudgetId,
    TransferId TransferId,
    TransferType Type,
    Money PreviousValue,
    Money NewValue
)
    : DomainEventBase;