using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedDefaultValueForColumnAccountStatementTypeOnTableAccountStatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountStatementType",
                table: "AccountStatements",
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountStatementType",
                table: "AccountStatements",
                defaultValue: 0);
        }
    }
}
