using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudget.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateTransfersTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Transfers",
            schema: "budget",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                BudgetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TransferDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                Value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                Currency = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Category = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transfers", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transfers_Budgets_BudgetId",
                    column: x => x.BudgetId,
                    principalSchema: "budget",
                    principalTable: "Budgets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Transfers_BudgetId",
            schema: "budget",
            table: "Transfers",
            column: "BudgetId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transfers",
            schema: "budget");
    }
}
