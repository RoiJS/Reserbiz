using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReserbizAPP.LIB.Migrations.ReserbizClientData
{
    public partial class IntroducedSpaceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    SpaceTypeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DeletedById = table.Column<int>(nullable: true),
                    UpdatedById = table.Column<int>(nullable: true),
                    CreatedById = table.Column<int>(nullable: true),
                    DeactivatedById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_CreatedById_Accounts_AccountId",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spaces_DeactivatedById_Accounts_AccountId",
                        column: x => x.DeactivatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spaces_DeletedById_Accounts_AccountId",
                        column: x => x.DeletedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spaces_SpaceTypes_SpaceTypeId",
                        column: x => x.SpaceTypeId,
                        principalTable: "SpaceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spaces_UpdatedById_Accounts_AccountId",
                        column: x => x.UpdatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_CreatedById",
                table: "Spaces",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DeactivatedById",
                table: "Spaces",
                column: "DeactivatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DeletedById",
                table: "Spaces",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_SpaceTypeId",
                table: "Spaces",
                column: "SpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_UpdatedById",
                table: "Spaces",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spaces");
        }
    }
}
