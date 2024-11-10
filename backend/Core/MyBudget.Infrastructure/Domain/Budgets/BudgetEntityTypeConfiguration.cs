using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Domain.Budgets;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Domain.Budgets;

internal sealed class BudgetEntityTypeConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets", SchemaName.Budget);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(256);

        builder.Property(x => x.CreationDate)
            .IsRequired();

        builder.Property(x => x.LastUpdated);

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(16);

        builder.OwnsMany(x => x.Categories, b =>
        {
            b.WithOwner()
                .HasForeignKey("BudgetId");

            b.ToTable("Categories", SchemaName.Budget);

            b.Property(x => x.Name)
                .HasMaxLength(32)
                .IsRequired();

            b.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(16);

            b.HasKey("BudgetId", "Name");
        });
    }
}