using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class SponsorSinglePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhoto");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_SponsorPhotoId",
                table: "Sponsors",
                column: "SponsorPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhoto",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_SponsorPhoto_SponsorPhotoId",
                table: "Sponsors",
                column: "SponsorPhotoId",
                principalTable: "SponsorPhoto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_SponsorPhoto_SponsorPhotoId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_SponsorPhotoId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhoto");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhoto",
                column: "SponsorId",
                unique: true);
        }
    }
}
