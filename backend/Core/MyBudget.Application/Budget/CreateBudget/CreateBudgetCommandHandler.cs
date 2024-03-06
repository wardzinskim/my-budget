using MassTransit.Mediator;
using MyBudget.Domain.Budget;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budget.CreateBudget;

public record CreateBudgetCommand(string Name) : Request<Result>;

public class CreateBudgetCommandHandler : MediatorRequestHandler<CreateBudgetCommand, Result>
{
    private IIdGenerator _idGenerator;
    private IDateTimeProvider _dateTimeProvider;
    private IBudgetRepository _budgetRepository;

    protected override async Task<Result> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = Domain.Budget.Budget.Create(_idGenerator, _dateTimeProvider, Guid.NewGuid(), request.Name);
        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await _budgetRepository.AddAsync(budgetResult.Value);

        return Result.Success();
    }
}