using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class RenameColumnGenerateAccountStatementDaysBeforeValueToBusinessName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenerateAccountStatementDaysBeforeValue",
                table: "ClientSettings");

            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "ClientSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "ClientSettings");

            migrationBuilder.AddColumn<int>(
                name: "GenerateAccountStatementDaysBeforeValue",
                table: "ClientSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
