using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.CreateBudget;

public record CreateBudgetCommand(string Name, string? Description) : Request<Result<Guid>>, ICommand;

public sealed class CreateBudgetCommandHandler(
    IBudgetRepository budgetRepository,
    IIdGenerator idGenerator,
    IBudgetNameUniquenessChecker budgetNameUniquenessChecker,
    IRequestContext requestContext
)
    : MediatorRequestHandler<CreateBudgetCommand, Result<Guid>>
{
    protected override async Task<Result<Guid>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = await Budget.Create(
                idGenerator,
                budgetNameUniquenessChecker,
                requestContext.UserId,
                request.Name,
                request.Description,
                cancellationToken)
            .ConfigureAwait(false);

        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await budgetRepository.AddAsync(budgetResult.Value, cancellationToken);

        return Result.Success(budgetResult.Value.Id.Value);
    }
}