using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.CreateBudget;

public record CreateBudgetCommand(string Name) : Request<Result>;

public class CreateBudgetCommandHandler : MediatorRequestHandler<CreateBudgetCommand, Result>
{
    private IIdGenerator _idGenerator;
    private IDateTimeProvider _dateTimeProvider;
    private IBudgetRepository _budgetRepository;

    protected override async Task<Result> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = Budget.Create(_idGenerator, _dateTimeProvider, Guid.NewGuid(), request.Name);
        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await _budgetRepository.AddAsync(budgetResult.Value, cancellationToken);

        return Result.Success();
    }
}