using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class MorePhotoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhotos_Photos_PhotoId",
                table: "SponsorPhotos");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "SponsorPhotos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SponsorPhotos",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "SponsorPhotos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "SponsorPhotos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoFile",
                table: "SponsorPhotos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "SponsorPhotos");

            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "SponsorPhotos");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "SponsorPhotos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorPhotos",
                table: "SponsorPhotos",
                column: "PhotoId");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageMimeType = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    PhotoFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorPhotos_Photos_PhotoId",
                table: "SponsorPhotos",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
