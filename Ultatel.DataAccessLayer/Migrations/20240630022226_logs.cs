using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultatel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Operation",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "OperationTime",
                table: "StudentLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "CreateAdminId",
                table: "StudentLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTimeStamps",
                table: "StudentLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateAdminId",
                table: "StudentLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTimeStamps",
                table: "StudentLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_CreateAdminId",
                table: "StudentLogs",
                column: "CreateAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_UpdateAdminId",
                table: "StudentLogs",
                column: "UpdateAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLogs_Admins_CreateAdminId",
                table: "StudentLogs",
                column: "CreateAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLogs_Admins_UpdateAdminId",
                table: "StudentLogs",
                column: "UpdateAdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLogs_Admins_CreateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLogs_Admins_UpdateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropIndex(
                name: "IX_StudentLogs_CreateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropIndex(
                name: "IX_StudentLogs_UpdateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "CreateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "CreateTimeStamps",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "UpdateAdminId",
                table: "StudentLogs");

            migrationBuilder.DropColumn(
                name: "UpdateTimeStamps",
                table: "StudentLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "StudentLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Operation",
                table: "StudentLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OperationTime",
                table: "StudentLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
