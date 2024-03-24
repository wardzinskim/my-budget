using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.Transfers.DeleteTransfer;

public record DeleteTransferCommand(Guid BudgetId, Guid TransferId) : Request<Result>, ICommand;

public sealed class DeleteTransferCommandHandler(IBudgetRepository budgetRepository, IRequestContext requestContext)
    : MediatorRequestHandler<DeleteTransferCommand, Result>
{
    protected override async Task<Result> Handle(DeleteTransferCommand request, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository
            .GetAsync(request.BudgetId,
                x => x.Id == request.TransferId,
                cancellationToken)
            .ConfigureAwait(false);
        if (budget is null)
        {
            return BudgetsErrors.BudgetNotFound;
        }

        var access = budget.HasAccess(requestContext.UserId);
        if (access.IsFailure)
        {
            return access.Error;
        }


        var result = budget.DeleteTransfer(request.TransferId);
        if (result.IsFailure) return result.Error;

        return Result.Success();
    }
}