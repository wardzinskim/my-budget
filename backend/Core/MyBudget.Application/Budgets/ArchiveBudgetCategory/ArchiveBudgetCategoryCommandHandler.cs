using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.ArchiveBudgetCategory;

public record ArchiveBudgetCategoryCommand(Guid BudgetId, string Name) : Request<Result>, ICommand;

public sealed class ArchiveBudgetCategoryCommandHandler : MediatorRequestHandler<ArchiveBudgetCategoryCommand, Result>
{
    private readonly IBudgetRepository _budgetRepository;

    public ArchiveBudgetCategoryCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    protected override async Task<Result> Handle(ArchiveBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetAsync(request.BudgetId, cancellationToken);
        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        var result = budget.ArchiveTransferCategory(request.Name);
        if (result.IsFailure)
            return result.Error;

        return Result.Success();
    }
}
