using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SavingId",
                table: "Savings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvestmentsId",
                table: "Investments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IncomeId",
                table: "Incomes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExpensesId",
                table: "Expenses",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Savings",
                newName: "SavingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Investments",
                newName: "InvestmentsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Incomes",
                newName: "IncomeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ExpensesId");
        }
    }
}
