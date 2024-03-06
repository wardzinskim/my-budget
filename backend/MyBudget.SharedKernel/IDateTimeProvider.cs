namespace MyBudget.SharedKernel;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}