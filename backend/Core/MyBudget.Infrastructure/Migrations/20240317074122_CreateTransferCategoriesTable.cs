using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudget.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateTransferCategoriesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            schema: "budget",
            columns: table => new
            {
                Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BudgetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Status = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => new { x.BudgetId, x.Name });
                table.ForeignKey(
                    name: "FK_Categories_Budgets_BudgetId",
                    column: x => x.BudgetId,
                    principalSchema: "budget",
                    principalTable: "Budgets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Categories",
            schema: "budget");
    }
}
