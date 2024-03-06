namespace MyBudget.Domain.Budget;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget);
}