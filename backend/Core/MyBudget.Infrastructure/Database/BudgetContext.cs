using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;

namespace MyBudget.Infrastructure.Database;

internal class BudgetContext : DbContext
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