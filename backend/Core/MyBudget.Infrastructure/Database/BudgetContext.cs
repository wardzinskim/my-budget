using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;

namespace MyBudget.Infrastructure.Database;

public class BudgetContext : DbContext
{
    public DbSet<Budget> Budgets { get; set; }

    public BudgetContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetContext).Assembly);
    }
}