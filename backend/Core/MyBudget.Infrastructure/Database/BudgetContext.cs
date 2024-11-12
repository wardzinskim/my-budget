using Microsoft.EntityFrameworkCore;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Domain.Shared;
using MyBudget.Infrastructure.Database.Conversions;

namespace MyBudget.Infrastructure.Database;

public class BudgetContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<BudgetId>()
            .HaveConversion<BudgetIdConverter>();

        configurationBuilder
            .Properties<TransferId>()
            .HaveConversion<TransferIdConverter>();

        configurationBuilder
            .Properties<UserId>()
            .HaveConversion<UserIdConverter>();
    }
}