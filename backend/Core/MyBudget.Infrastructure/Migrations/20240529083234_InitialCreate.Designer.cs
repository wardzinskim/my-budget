﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBudget.Infrastructure.Database;

#nullable disable

namespace MyBudget.Infrastructure.Migrations
{
    [DbContext(typeof(BudgetContext))]
    [Migration("20240529083234_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyBudget.Domain.Budgets.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Budgets", "budget");
                });

            modelBuilder.Entity("MyBudget.Domain.Budgets.Budget", b =>
                {
                    b.OwnsMany("MyBudget.Domain.Budgets.TransferCategory", "Categories", b1 =>
                        {
                            b1.Property<Guid>("BudgetId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .HasMaxLength(32)
                                .HasColumnType("nvarchar(32)");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.HasKey("BudgetId", "Name");

                            b1.ToTable("Categories", "budget");

                            b1.WithOwner()
                                .HasForeignKey("BudgetId");
                        });

                    b.OwnsMany("MyBudget.Domain.Budgets.Transfers.Transfer", "Transfers", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("BudgetId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Category")
                                .HasMaxLength(32)
                                .HasColumnType("nvarchar(32)");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("LastUpdated")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("nvarchar(128)");

                            b1.Property<DateTime>("TransferDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.HasKey("Id");

                            b1.HasIndex("BudgetId");

                            b1.ToTable("Transfers", "budget");

                            b1.WithOwner()
                                .HasForeignKey("BudgetId");

                            b1.OwnsOne("MyBudget.Domain.Shared.Money", "Value", b2 =>
                                {
                                    b2.Property<Guid>("TransferId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasMaxLength(8)
                                        .HasColumnType("nvarchar(8)")
                                        .HasColumnName("Currency");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("Value");

                                    b2.HasKey("TransferId");

                                    b2.ToTable("Transfers", "budget");

                                    b2.WithOwner()
                                        .HasForeignKey("TransferId");
                                });

                            b1.Navigation("Value")
                                .IsRequired();
                        });

                    b.Navigation("Categories");

                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}