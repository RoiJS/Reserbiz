using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedMiscellaneousDueDateEnumColumnOnAccountStatementsAndTermsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MiscellaneousDueDate",
                table: "Terms",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "MiscellaneousDueDate",
                table: "AccountStatements",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiscellaneousDueDate",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "MiscellaneousDueDate",
                table: "AccountStatements");
        }
    }
}
