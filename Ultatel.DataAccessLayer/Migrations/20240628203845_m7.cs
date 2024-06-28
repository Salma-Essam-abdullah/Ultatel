using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultatel.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminStudent_Admins_AdminsId",
                table: "AdminStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminStudent_Students_StudentsId",
                table: "AdminStudent");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "AdminStudent",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "AdminsId",
                table: "AdminStudent",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminStudent_StudentsId",
                table: "AdminStudent",
                newName: "IX_AdminStudent_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminStudent_Admins_AdminId",
                table: "AdminStudent",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminStudent_Students_StudentId",
                table: "AdminStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminStudent_Admins_AdminId",
                table: "AdminStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminStudent_Students_StudentId",
                table: "AdminStudent");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "AdminStudent",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "AdminStudent",
                newName: "AdminsId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminStudent_StudentId",
                table: "AdminStudent",
                newName: "IX_AdminStudent_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminStudent_Admins_AdminsId",
                table: "AdminStudent",
                column: "AdminsId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminStudent_Students_StudentsId",
                table: "AdminStudent",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
