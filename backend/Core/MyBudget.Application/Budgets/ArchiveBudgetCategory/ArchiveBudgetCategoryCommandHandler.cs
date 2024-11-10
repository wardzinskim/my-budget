using MyBudget.Application.Budgets.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.ArchiveBudgetCategory;

public record ArchiveBudgetCategoryCommand(Guid BudgetId, string Name) : Request<Result>, ICommand;

public sealed class ArchiveBudgetCategoryCommandHandler(
    IBudgetRepository budgetRepository,
    IBudgetAccessValidator budgetAccessValidator
) : MediatorRequestHandler<ArchiveBudgetCategoryCommand, Result>
{
    protected override async Task<Result> Handle(
        ArchiveBudgetCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var budget = await budgetRepository.GetAsync(request.BudgetId, cancellationToken);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.HasUserAccess(budget);
        if (access.IsFailure) return access.Error;

        var result = budget.ArchiveTransferCategory(request.Name);
        if (result.IsFailure) return result.Error;

        return Result.Success();
    }
}