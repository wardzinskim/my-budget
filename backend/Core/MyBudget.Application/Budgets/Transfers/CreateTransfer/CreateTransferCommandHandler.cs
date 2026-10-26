using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.Transfers.CreateTransfer;

public record CreateTransferCommand(
    Guid BudgetId,
    TransferDTOType Type,
    string Name,
    decimal Value,
    string Currency,
    string? Category = null,
    DateTime? Date = null
) : Request<Result<TransferDTO>>, ICommand;

public sealed class CreateTransferCommandHandler(
    IBudgetRepository budgetRepository,
    ITransferRepository transferRepository,
    IBudgetAccessValidator budgetAccessValidator,
    IIdGenerator idGenerator,
    IDateTimeProvider dateProvider
)
    : MediatorRequestHandler<CreateTransferCommand, Result<TransferDTO>>
{
    protected override async Task<Result<TransferDTO>> Handle(
        CreateTransferCommand request,
        CancellationToken cancellationToken
    )
    {
        var budget = await budgetRepository.GetAsync(request.BudgetId, cancellationToken).ConfigureAwait(false);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var transfer = budget.AddTransfer(idGenerator, (TransferType)request.Type,
            new(request.Name, request.Value, request.Currency, request.Date ?? dateProvider.UtcNow, request.Category));
        if (transfer.IsFailure) return transfer.Error;

        await transferRepository.AddAsync(transfer.Value, cancellationToken);

        return new TransferDTO(
            transfer.Value.Id,
            transfer.Value.TransferDate,
            transfer.Value.Value.Value,
            transfer.Value.Value.Currency,
            (TransferDTOType)transfer.Value.Type,
            transfer.Value.Name,
            transfer.Value.Category
        );
    }
}