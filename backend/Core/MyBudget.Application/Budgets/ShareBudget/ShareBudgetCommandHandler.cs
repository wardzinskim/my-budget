using MyBudget.Application.Budgets.Services;
using MyBudget.Application.Services;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;

namespace MyBudget.Application.Budgets.ShareBudget;

public record ShareBudgetCommand(Guid BudgetId, string UserLogin) : Request<Result>, ICommand;

public class ShareBudgetCommandHandler(
    IBudgetRepository budgetRepository,
    IBudgetAccessValidator budgetAccessValidator,
    IUserService userService
) : MediatorRequestHandler<ShareBudgetCommand, Result>
{
    protected override async Task<Result> Handle(ShareBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.GetAsync(request.BudgetId, cancellationToken);
        if (budget is null) return BudgetsErrors.BudgetNotFound;

        var access = budgetAccessValidator.IsOwner(budget);
        if (access.IsFailure) return access.Error;

        var userResult = await userService.FindUserAsync(request.UserLogin, cancellationToken);

        if (userResult.IsFailure) return userResult.Error;

        var result = budget.Share(userResult.Value.Id, userResult.Value.Login);
        if (result.IsFailure) return result.Error;

        return Result.Success();
    }
}