using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class SponsorPhoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhotos_Sponsors_SponsorId",
                table: "SponsorPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos");

            migrationBuilder.RenameTable(
                name: "SponsorPhotos",
                newName: "SponsorPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhoto",
                newName: "IX_SponsorPhoto_SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhoto",
                table: "SponsorPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhoto_Sponsors_SponsorId",
                table: "SponsorPhoto",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhoto_Sponsors_SponsorId",
                table: "SponsorPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhoto",
                table: "SponsorPhoto");

            migrationBuilder.RenameTable(
                name: "SponsorPhoto",
                newName: "SponsorPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhotos",
                newName: "IX_SponsorPhotos_SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhotos_Sponsors_SponsorId",
                table: "SponsorPhotos",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
