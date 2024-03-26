namespace MyBudget.SharedKernel;

public interface IAsyncBusinessRule
{
    Task<Result> ValidateAsync(CancellationToken cancellationToken = default);
}

public interface IBusinessRule
{
    Result Validate();
}