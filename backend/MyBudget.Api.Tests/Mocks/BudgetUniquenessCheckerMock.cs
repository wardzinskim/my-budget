using MyBudget.Domain.Budgets;

namespace MyBudget.Api.Tests.Mocks;

internal class BudgetUniquenessCheckerMock : IBudgetNameUniquenessChecker
{
    public Task<bool> IsUniqueAsync(Guid ownerId, string name, CancellationToken cancellationToken = default)
        => Task.FromResult(false);
}