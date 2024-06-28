using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultatel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLogs_AspNetUsers_UserId",
                table: "StudentLogs");

            migrationBuilder.DropIndex(
                name: "IX_StudentLogs_UserId",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "StudentLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_AdminId",
                table: "StudentLogs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLogs_Admins_AdminId",
                table: "StudentLogs",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLogs_Admins_AdminId",
                table: "StudentLogs");

            migrationBuilder.DropIndex(
                name: "IX_StudentLogs_AdminId",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "StudentLogs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_UserId",
                table: "StudentLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLogs_AspNetUsers_UserId",
                table: "StudentLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
