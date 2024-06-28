using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultatel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Admins_AdminId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AdminId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "AdminStudent",
                columns: table => new
                {
                    AdminsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminStudent", x => new { x.AdminsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_AdminStudent_Admins_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminStudent_StudentsId",
                table: "AdminStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminStudent");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdminId",
                table: "Students",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Admins_AdminId",
                table: "Students",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
