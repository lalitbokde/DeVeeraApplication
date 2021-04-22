using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ChangeIncomeAboveOrBelow80KCollumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IncomeAboveOrBelow80K",
                table: "User",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IncomeAboveOrBelow80K",
                table: "User",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
