using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhoto_Photo_PhotoId",
                table: "SponsorPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhoto_Sponsors_SponsorId",
                table: "SponsorPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhoto",
                table: "SponsorPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "SponsorPhoto",
                newName: "SponsorPhotos");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorPhoto_SponsorId",
                table: "SponsorPhotos",
                newName: "IX_SponsorPhotos_SponsorId");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Sponsors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sponsors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sponsors",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos",
                column: "PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhotos_Photos_PhotoId",
                table: "SponsorPhotos",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhotos_Sponsors_SponsorId",
                table: "SponsorPhotos",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhotos_Photos_PhotoId",
                table: "SponsorPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhotos_Sponsors_SponsorId",
                table: "SponsorPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "SponsorPhotos",
                newName: "SponsorPhoto");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorPhotos_SponsorId",
                table: "SponsorPhoto",
                newName: "IX_SponsorPhoto_SponsorId");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Sponsors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sponsors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sponsors",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhoto",
                table: "SponsorPhoto",
                column: "PhotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhoto_Photo_PhotoId",
                table: "SponsorPhoto",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhoto_Sponsors_SponsorId",
                table: "SponsorPhoto",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
