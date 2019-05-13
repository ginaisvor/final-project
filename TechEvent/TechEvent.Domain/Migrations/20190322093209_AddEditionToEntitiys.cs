using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class AddEditionToEntitiys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {/*
            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "Workshops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "TalkTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "Talks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "SponsorTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "Sponsors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "Speakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_EditionId",
                table: "Workshops",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TalkTypes_EditionId",
                table: "TalkTypes",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_EditionId",
                table: "Talks",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorTypes_EditionId",
                table: "SponsorTypes",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_EditionId",
                table: "Sponsors",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_EditionId",
                table: "Speakers",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Editions_EditionId",
                table: "Speakers",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_Editions_EditionId",
                table: "Sponsors",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorTypes_Editions_EditionId",
                table: "SponsorTypes",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Talks_Editions_EditionId",
                table: "Talks",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TalkTypes_Editions_EditionId",
                table: "TalkTypes",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_Editions_EditionId",
                table: "Workshops",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Editions_EditionId",
                table: "Speakers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_Editions_EditionId",
                table: "Sponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorTypes_Editions_EditionId",
                table: "SponsorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Talks_Editions_EditionId",
                table: "Talks");

            migrationBuilder.DropForeignKey(
                name: "FK_TalkTypes_Editions_EditionId",
                table: "TalkTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Editions_EditionId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_EditionId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_TalkTypes_EditionId",
                table: "TalkTypes");

            migrationBuilder.DropIndex(
                name: "IX_Talks_EditionId",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_SponsorTypes_EditionId",
                table: "SponsorTypes");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_EditionId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_EditionId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "TalkTypes");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "SponsorTypes");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Speakers");
        }
    }
}
