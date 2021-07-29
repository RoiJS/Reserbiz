using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations
{
    public partial class RenamedColumnAppWebAPIDomainURLOnTableAppGlobalSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppWebAPIDomain",
                table: "AppGlobalSettings",
                newName: "AppWebAPIDomainURL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppWebAPIDomainURL",
                table: "AppGlobalSettings",
                newName: "AppWebAPIDomain");
        }
    }
}
