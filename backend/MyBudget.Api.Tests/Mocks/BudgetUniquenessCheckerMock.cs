using MyBudget.Domain.Budgets;
using MyBudget.Domain.Shared;

namespace MyBudget.Api.Tests.Mocks;

internal class BudgetUniquenessCheckerMock : IBudgetNameUniquenessChecker
{
    public Task<bool> IsUniqueAsync(UserId ownerId, string name, CancellationToken cancellationToken = default)
        => Task.FromResult(false);
}