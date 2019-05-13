using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class AddPapers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyWebsite",
                table: "Papers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Papers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherMentions",
                table: "Papers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaperStatusId",
                table: "Papers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PresentationDescription",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PresentationTitle",
                table: "Papers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skype",
                table: "Papers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Papers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TalkTypeId",
                table: "Papers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Papers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Papers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Papers_PaperStatusId",
                table: "Papers",
                column: "PaperStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Papers_TalkTypeId",
                table: "Papers",
                column: "TalkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_PaperStatuses_PaperStatusId",
                table: "Papers",
                column: "PaperStatusId",
                principalTable: "PaperStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_TalkTypes_TalkTypeId",
                table: "Papers",
                column: "TalkTypeId",
                principalTable: "TalkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_PaperStatuses_PaperStatusId",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_Papers_TalkTypes_TalkTypeId",
                table: "Papers");

            migrationBuilder.DropIndex(
                name: "IX_Papers_PaperStatusId",
                table: "Papers");

            migrationBuilder.DropIndex(
                name: "IX_Papers_TalkTypeId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "CompanyWebsite",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "OtherMentions",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PaperStatusId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PresentationDescription",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PresentationTitle",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Skype",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "TalkTypeId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Papers");
        }
    }
}
