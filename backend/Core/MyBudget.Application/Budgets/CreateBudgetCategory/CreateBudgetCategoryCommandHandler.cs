using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.CreateBudgetCategory;

public record CreateBudgetCategoryCommand(Guid BudgetId, string Name) : Request<Result>, ICommand;

public sealed class CreateBudgetCategoryCommandHandler(IBudgetRepository budgetRepository)
    : MediatorRequestHandler<CreateBudgetCategoryCommand, Result>
{
    protected override async Task<Result> Handle(
        CreateBudgetCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var budget = await budgetRepository.GetAsync(request.BudgetId, cancellationToken).ConfigureAwait(false);

        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        var result = budget.AddTransferCategory(request.Name);
        if (result.IsFailure)
            return result.Error;

        return Result.Success();
    }
}