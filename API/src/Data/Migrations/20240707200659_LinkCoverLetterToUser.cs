using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoverLetterGeneratorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkCoverLetterToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CoverLetters",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CoverLetters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoverLetters_UserId",
                table: "CoverLetters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverLetters_AspNetUsers_UserId",
                table: "CoverLetters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverLetters_AspNetUsers_UserId",
                table: "CoverLetters");

            migrationBuilder.DropIndex(
                name: "IX_CoverLetters_UserId",
                table: "CoverLetters");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CoverLetters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CoverLetters");
        }
    }
}
