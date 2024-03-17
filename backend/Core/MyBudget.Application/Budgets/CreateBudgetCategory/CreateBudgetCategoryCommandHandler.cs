using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.CreateBudgetCategory;

public record CreateBudgetCategoryCommand(Guid BudgetId, string Name) : Request<Result>, ICommand;

public sealed class CreateBudgetCategoryCommandHandler : MediatorRequestHandler<CreateBudgetCategoryCommand, Result>
{
    private readonly IBudgetRepository _budgetRepository;

    public CreateBudgetCategoryCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    protected override async Task<Result> Handle(
        CreateBudgetCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var budget = await _budgetRepository.GetAsync(request.BudgetId, cancellationToken).ConfigureAwait(false);

        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        budget.AddTransferCategory(request.Name);

        return Result.Success();
    }
}