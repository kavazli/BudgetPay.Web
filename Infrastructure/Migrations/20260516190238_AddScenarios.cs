using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScenarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Company = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    ScenarioName = table.Column<string>(type: "TEXT", nullable: false),
                    EconomicIndicator = table.Column<decimal>(type: "TEXT", nullable: false),
                    WelfarShare = table.Column<decimal>(type: "TEXT", nullable: false),
                    RafeOfIncrase = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_50 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Overtime_100 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bonus = table.Column<decimal>(type: "TEXT", nullable: false),
                    BonusMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucher = table.Column<decimal>(type: "TEXT", nullable: false),
                    ShoppingVoucherMonth = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scenarios");
        }
    }
}
