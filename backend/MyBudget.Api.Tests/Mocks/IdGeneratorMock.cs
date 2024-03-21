using MyBudget.SharedKernel;

namespace MyBudget.Api.Tests.Mocks;

internal class IdGeneratorMock(Guid Id) : IIdGenerator
{
    public Guid NextId() => Id;
}