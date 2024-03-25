using MassTransit.Mediator;
using MyBudget.Application.Budgets.Model;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.CreateTransfer;

public record CreateTransferCommand(
    Guid BudgetId,
    TransferDTOType Type,
    string Name,
    decimal Value,
    string Currency,
    DateTime? Date = null
)
    : Request<Result>, ICommand;

public sealed class CreateTransferCommandHandler : MediatorRequestHandler<CreateTransferCommand, Result>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IRequestContext _requestContext;
    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeProvider _dateProvider;

    public CreateTransferCommandHandler(
        IBudgetRepository budgetRepository,
        IRequestContext requestContext,
        IIdGenerator idGenerator,
        IDateTimeProvider dateProvider
    )
    {
        _budgetRepository = budgetRepository;
        _requestContext = requestContext;
        _idGenerator = idGenerator;
        _dateProvider = dateProvider;
    }

    protected override async Task<Result> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetAsync(request.BudgetId, cancellationToken).ConfigureAwait(false);
        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        var access = budget.HasAccess(_requestContext.UserId);
        if (access.IsFailure)
        {
            return access.Error;
        }

        var transfer = budget.AddTransfer(_idGenerator, (TransferType)request.Type,
            new(request.Name, request.Value, request.Currency, request.Date ?? _dateProvider.UtcNow));
        if (transfer.IsFailure)
        {
            return transfer.Error;
        }

        return Result.Success();
    }
}