using MassTransit;
using MyBudget.SharedKernel;

namespace MyBudget.Api.SharedKernel;

public sealed class IdGenerator : IIdGenerator
{
    public Guid NextId() => NewId.NextSequentialGuid();
}
