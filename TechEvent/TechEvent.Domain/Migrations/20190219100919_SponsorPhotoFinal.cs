using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class SponsorPhotoFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhotos");

            migrationBuilder.AddColumn<int>(
                name: "SponsorPhotoId",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhotos",
                column: "SponsorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "SponsorPhotoId",
                table: "Sponsors");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhotos",
                column: "SponsorId");
        }
    }
}
