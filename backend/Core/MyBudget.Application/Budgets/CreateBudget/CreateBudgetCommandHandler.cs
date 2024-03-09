using MassTransit.Mediator;
using MyBudget.Domain.Budgets;
using MyBudget.SharedKernel;
using System.Xml.Serialization;

namespace MyBudget.Application.Budgets.CreateBudget;

public record CreateBudgetCommand(string Name) : Request<Result>;

public class CreateBudgetCommandHandler : MediatorRequestHandler<CreateBudgetCommand, Result>
{
    private Guid OwnerId = Guid.Parse("9e56ca7a-671b-48ff-a739-8f35acdf58a3");

    private readonly IIdGenerator _idGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IBudgetNameUniquenessChecker _budgetNameUniquenessChecker;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetCommandHandler(
        IBudgetRepository budgetRepository,
        IIdGenerator idGenerator,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        IBudgetNameUniquenessChecker budgetNameUniquenessChecker
    )
    {
        _budgetRepository = budgetRepository;
        _idGenerator = idGenerator;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _budgetNameUniquenessChecker = budgetNameUniquenessChecker;
    }

    protected override async Task<Result> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budgetResult = await Budget.Create(
                _idGenerator,
                _dateTimeProvider,
                _budgetNameUniquenessChecker,
                OwnerId,
                request.Name,
                cancellationToken)
            .ConfigureAwait(false);
        
        if (budgetResult.IsFailure)
            return budgetResult.Error;

        await _budgetRepository.AddAsync(budgetResult.Value, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}