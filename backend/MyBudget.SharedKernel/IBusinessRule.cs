namespace MyBudget.SharedKernel;

public interface IBusinessRule
{
    ValueTask<Result> ValidateAsync(CancellationToken cancellationToken = default);
}