using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechEvent.Domain.Migrations
{
    public partial class IdentityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {/*
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhoto_Sponsors_SponsorId",
                table: "SponsorPhoto");
                */
                /*
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Workshops",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workshops",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Talks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Talks",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
*/
            migrationBuilder.AlterColumn<string>(
                name: "LinkedIn",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GitHub",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });
/*
            migrationBuilder.CreateTable(
                name: "SpeakerPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpeakerId = table.Column<int>(nullable: false),
                    ImageMimeType = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    PhotoFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerPhoto_Speakers",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
*/
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
/*
            migrationBuilder.CreateIndex(
                name: "IX_Workshops_SpeakerId",
                table: "Workshops",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_TalkTypeId",
                table: "Workshops",
                column: "TalkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_SpeakerId",
                table: "Talks",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_TalkTypeId",
                table: "Talks",
                column: "TalkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Papers_EditionId",
                table: "Papers",
                column: "EditionId");
*/
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
            /*
                        migrationBuilder.CreateIndex(
                            name: "IX_SpeakerPhoto_SpeakerId",
                            table: "SpeakerPhoto",
                            column: "SpeakerId",
                            unique: true);

                        migrationBuilder.AddForeignKey(
                            name: "FK_Papers_Editions",
                            table: "Papers",
                            column: "EditionId",
                            principalTable: "Editions",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);

                        migrationBuilder.AddForeignKey(
                            name: "FK_SponsorPhoto_Sponsors",
                            table: "SponsorPhoto",
                            column: "SponsorId",
                            principalTable: "Sponsors",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);

                        migrationBuilder.AddForeignKey(
                            name: "FK_Talks_Speakers",
                            table: "Talks",
                            column: "SpeakerId",
                            principalTable: "Speakers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);

                        migrationBuilder.AddForeignKey(
                            name: "FK_Talks_TalkTypes",
                            table: "Talks",
                            column: "TalkTypeId",
                            principalTable: "TalkTypes",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);

                        migrationBuilder.AddForeignKey(
                            name: "FK_Workshops_Speakers",
                            table: "Workshops",
                            column: "SpeakerId",
                            principalTable: "Speakers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);

                        migrationBuilder.AddForeignKey(
                            name: "FK_Workshops_TalkTypes",
                            table: "Workshops",
                            column: "TalkTypeId",
                            principalTable: "TalkTypes",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Restrict);
                            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Editions",
                table: "Papers");

            migrationBuilder.DropForeignKey(
                name: "FK_SponsorPhoto_Sponsors",
                table: "SponsorPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Talks_Speakers",
                table: "Talks");

            migrationBuilder.DropForeignKey(
                name: "FK_Talks_TalkTypes",
                table: "Talks");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_Speakers",
                table: "Workshops");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_TalkTypes",
                table: "Workshops");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SpeakerPhoto");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_SpeakerId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_TalkTypeId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Talks_SpeakerId",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_Talks_TalkTypeId",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_Papers_EditionId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Talks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Talks");

            migrationBuilder.AlterColumn<string>(
                name: "LinkedIn",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "GitHub",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(string));

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
