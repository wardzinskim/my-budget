using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.SharedKernel;

namespace MyBudget.Application.Budgets.CreateBudget;

public record CreateBudgetCommand(string Name) : Request<Result>;

public class CreateBudgetCommandHandler : MediatorRequestHandler<CreateBudgetCommand, Result>
{
    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetCommandHandler(IBudgetRepository budgetRepository, IIdGenerator idGenerator, IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork)
    {
        _budgetRepository = budgetRepository;
        _idGenerator = idGenerator;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<Result> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = Budget.Create(_idGenerator, _dateTimeProvider, Guid.NewGuid(), request.Name);
        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await _budgetRepository.AddAsync(budgetResult.Value, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}