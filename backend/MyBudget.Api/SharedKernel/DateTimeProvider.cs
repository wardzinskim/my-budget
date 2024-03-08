using MyBudget.SharedKernel;

namespace MyBudget.Api.SharedKernel;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
