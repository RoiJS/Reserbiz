using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class AddNewPenaltyRelatedPropertiesOnAccountStatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Penalty",
                table: "AccountStatements",
                newName: "PenaltyValue");

            migrationBuilder.AddColumn<int>(
                name: "PenaltyAmountPerDurationUnit",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyEffectiveAfterDurationUnit",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyEffectiveAfterDurationValue",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenaltyValueType",
                table: "AccountStatements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyAmountPerDurationUnit",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "PenaltyEffectiveAfterDurationUnit",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "PenaltyEffectiveAfterDurationValue",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "PenaltyValueType",
                table: "AccountStatements");

            migrationBuilder.RenameColumn(
                name: "PenaltyValue",
                table: "AccountStatements",
                newName: "Penalty");
        }
    }
}
