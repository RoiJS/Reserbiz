using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class RenameHasSentSMSNotificationForTheDayToSMSNotificationLastDateSentColumnOnTableAccountStatementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSentSMSNotificationForTheDay",
                table: "AccountStatements");

            migrationBuilder.AddColumn<DateTime>(
                name: "SMSNotificationLastDateSent",
                table: "AccountStatements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SMSNotificationLastDateSent",
                table: "AccountStatements");

            migrationBuilder.AddColumn<bool>(
                name: "HasSentSMSNotificationForTheDay",
                table: "AccountStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
