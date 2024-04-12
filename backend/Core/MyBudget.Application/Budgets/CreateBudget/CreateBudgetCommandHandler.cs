using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.CreateBudget;

public record CreateBudgetCommand(string Name, string? Description) : Request<Result>, ICommand;

public sealed class CreateBudgetCommandHandler : MediatorRequestHandler<CreateBudgetCommand, Result>
{
    private readonly IRequestContext _requestContext;
    private readonly IIdGenerator _idGenerator;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IBudgetNameUniquenessChecker _budgetNameUniquenessChecker;

    public CreateBudgetCommandHandler(
        IBudgetRepository budgetRepository,
        IIdGenerator idGenerator,
        IBudgetNameUniquenessChecker budgetNameUniquenessChecker,
        IRequestContext requestContext
    )
    {
        _budgetRepository = budgetRepository;
        _idGenerator = idGenerator;
        _budgetNameUniquenessChecker = budgetNameUniquenessChecker;
        _requestContext = requestContext;
    }

    protected override async Task<Result> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = await Budget.Create(
                _idGenerator,
                _budgetNameUniquenessChecker,
                _requestContext.UserId,
                request.Name,
                request.Description,
                cancellationToken)
            .ConfigureAwait(false);

        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await _budgetRepository.AddAsync(budgetResult.Value, cancellationToken);

        return Result.Success();
    }
}