using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;

namespace MyBudget.Infrastructure.Database;

public class BudgetContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetContext).Assembly);
    }
}