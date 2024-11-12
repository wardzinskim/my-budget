using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Domain.Shared;
using MyBudget.Infrastructure.Database;

namespace MyBudget.Infrastructure.Domain.Budgets.Transfers;

public class TransferEntityTypeConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("transfers", SchemaName.Budget);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasKey(x => new {x.Id, x.BudgetId});

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(16);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(16);

        builder.Property(x => x.TransferDate)
            .IsRequired();

        builder.Property(x => x.CreationDate)
            .IsRequired();

        builder.Property(x => x.LastUpdated);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Category)
            .HasMaxLength(32);

        builder.OwnsOne(x => x.Value, m =>
        {
            m.Property(x => x.Currency)
                .HasColumnName(nameof(Money.Currency).ToLower())
                .HasMaxLength(8);

            m.Property(x => x.Value)
                .HasColumnName(nameof(Money.Value).ToLower());
        });

        builder.HasQueryFilter(x => x.Status != TransferStatus.Deleted);
    }
}