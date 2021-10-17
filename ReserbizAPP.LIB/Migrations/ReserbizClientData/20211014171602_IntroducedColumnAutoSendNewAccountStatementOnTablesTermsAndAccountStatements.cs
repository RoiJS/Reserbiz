using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedColumnAutoSendNewAccountStatementOnTablesTermsAndAccountStatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoSendNewAccountStatement",
                table: "Terms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AutoSendNewAccountStatement",
                table: "AccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoSendNewAccountStatement",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "AutoSendNewAccountStatement",
                table: "AccountStatements");
        }
    }
}
