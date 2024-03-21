using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.CreateExpense;

public record CreateExpenseCommand(Guid BudgetId, string Name, decimal Value, string Currency, DateTime? Date = null)
    : Request<Result>, ICommand;

public sealed class CreateExpenseCommandHandler : MediatorRequestHandler<CreateExpenseCommand, Result>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IRequestContext _requestContext;
    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeProvider _dateProvider;

    public CreateExpenseCommandHandler(
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

    protected override async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
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

        var transfer = budget.AddExpense(_idGenerator, request.Name, request.Value, request.Currency,
            request.Date ?? _dateProvider.UtcNow);
        if (transfer.IsFailure)
        {
            return transfer.Error;
        }

        return Result.Success();
    }
}