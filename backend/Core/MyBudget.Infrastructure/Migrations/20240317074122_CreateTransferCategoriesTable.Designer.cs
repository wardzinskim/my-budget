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
    [Migration("20240317074122_CreateTransferCategoriesTable")]
    partial class CreateTransferCategoriesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MyBudget.Domain.Budgets.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Budgets", "budget");
                });

            modelBuilder.Entity("MyBudget.Domain.Budgets.Budget", b =>
                {
                    b.OwnsMany("MyBudget.Domain.Budgets.TransferCategory", "Categories", b1 =>
                        {
                            b1.Property<Guid>("BudgetId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Name")
                                .HasMaxLength(32)
                                .HasColumnType("varchar(32)");

                            b1.Property<string>("Status")
                                .IsRequired()
                                .HasMaxLength(16)
                                .HasColumnType("varchar(16)");

                            b1.HasKey("BudgetId", "Name");

                            b1.ToTable("Categories", "budget");

                            b1.WithOwner()
                                .HasForeignKey("BudgetId");
                        });

                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
