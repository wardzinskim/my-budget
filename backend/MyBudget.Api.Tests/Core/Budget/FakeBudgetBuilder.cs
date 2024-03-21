using MyBudget.Api.Tests.Mocks;

namespace MyBudget.Api.Tests.Core.Budget;

internal class FakeBudgetBuilder
{
    public static Domain.Budgets.Budget Build(Guid id, Guid ownerId, string name)
    {
        var response = Domain.Budgets.Budget
            .Create(new IdGeneratorMock(id), new BudgetUniquenessCheckerMock(), ownerId, name).GetAwaiter().GetResult();
        return response.Value;
    }
}