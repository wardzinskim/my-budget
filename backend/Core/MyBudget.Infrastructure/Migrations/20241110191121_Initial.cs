using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "budget");

            migrationBuilder.CreateTable(
                name: "budgets",
                schema: "budget",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_budgets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                schema: "budget",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    budget_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transfer_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    value = table.Column<decimal>(type: "numeric", nullable: false),
                    currency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    category = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfers", x => new { x.id, x.budget_id });
                });

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "budget",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    budget_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => new { x.budget_id, x.name });
                    table.ForeignKey(
                        name: "fk_categories_budgets_budget_id",
                        column: x => x.budget_id,
                        principalSchema: "budget",
                        principalTable: "budgets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "transfers",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "budgets",
                schema: "budget");
        }
    }
}
