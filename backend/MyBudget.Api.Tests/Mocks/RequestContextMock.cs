using MyBudget.SharedKernel;

namespace MyBudget.Api.Tests.Mocks;

internal class RequestContextMock : IRequestContext
{
    public Guid UserId => Guid.Parse("261d2b6e-e53d-4d40-b1ec-234016d8f69b");
}