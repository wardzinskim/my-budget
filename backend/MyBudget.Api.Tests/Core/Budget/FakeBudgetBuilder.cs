using MyBudget.Api.Tests.Mocks;

namespace MyBudget.Api.Tests.Core.Budget;

internal class FakeBudgetBuilder
{
    public static Domain.Budgets.Budget Build(Guid id, Guid ownerId, string name, string? description = null)
    {
        var response = Domain.Budgets.Budget
            .Create(new IdGeneratorMock(id), new BudgetUniquenessCheckerMock(), ownerId, name, description).GetAwaiter().GetResult();
        return response.Value;
    }
}