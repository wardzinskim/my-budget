using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Shared;
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


        builder.OwnsMany(x => x.Transfers, b =>
        {
            b.WithOwner()
                .HasForeignKey(x => x.BudgetId);

            b.ToTable("Transfers", SchemaName.Budget);

            b.Property(x => x.Id)
                .ValueGeneratedNever();

            b.HasKey(x => x.Id);

            b.Property(x => x.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(16);

            b.Property(x => x.TransferDate)
                .IsRequired();

            b.Property(x => x.CreationDate)
                .IsRequired();

            b.Property(x => x.LastUpdated);

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.Category)
                .HasMaxLength(32);

            b.OwnsOne(x => x.Value, m =>
            {
                m.Property(x => x.Currency)
                    .HasColumnName(nameof(Money.Currency))
                    .HasMaxLength(8);

                m.Property(x => x.Value)
                    .HasColumnName(nameof(Money.Value));
            });
        });

        builder.Navigation(x => x.Transfers)
            .AutoInclude(false);

    }
}