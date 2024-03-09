namespace MyBudget.SharedKernel;

public interface IRequestContext
{
    Guid UserId { get; }
}