using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroduceExcludeWaterBillAndExcludeElectricBillOnTableAccountStatementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExcludeElectricBill",
                table: "AccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ExcludeWaterBill",
                table: "AccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcludeElectricBill",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "ExcludeWaterBill",
                table: "AccountStatements");
        }
    }
}
