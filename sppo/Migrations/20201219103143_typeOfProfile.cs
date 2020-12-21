using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class typeOfProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forms_users_ProfileId1",
                table: "forms");

            migrationBuilder.DropIndex(
                name: "IX_forms_ProfileId1",
                table: "forms");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "forms");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "forms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_forms_ProfileId",
                table: "forms",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_forms_AspNetUsers_ProfileId",
                table: "forms",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_forms_AspNetUsers_ProfileId",
                table: "forms");

            migrationBuilder.DropIndex(
                name: "IX_forms_ProfileId",
                table: "forms");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "forms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId1",
                table: "forms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_forms_ProfileId1",
                table: "forms",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_forms_users_ProfileId1",
                table: "forms",
                column: "ProfileId1",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
