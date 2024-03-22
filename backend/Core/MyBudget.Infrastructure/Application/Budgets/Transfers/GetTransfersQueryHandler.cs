using MassTransit.Mediator;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using MyBudget.Infrastructure.Database;
using MyBudget.SharedKernel;

namespace MyBudget.Infrastructure.Application.Budgets.Transfers;

public sealed class GetTransfersQueryHandler : MediatorRequestHandler<GetTransfersQuery, Result>
{
    private readonly BudgetContext _dbContext;

    public GetTransfersQueryHandler(BudgetContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override Task<Result> Handle(GetTransfersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }
}