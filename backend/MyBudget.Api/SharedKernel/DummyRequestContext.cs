using MyBudget.SharedKernel;

namespace MyBudget.Api.SharedKernel;

public sealed class DummyRequestContext : IRequestContext
{
    public Guid UserId { get; } = Guid.Parse("9e56ca7a-671b-48ff-a739-8f35acdf58a3");
}