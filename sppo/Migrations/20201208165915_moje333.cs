using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class moje333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId1",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ProfileId1",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "reviews",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ProfileId",
                table: "reviews",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId",
                table: "reviews",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ProfileId",
                table: "reviews");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId1",
                table: "reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ProfileId1",
                table: "reviews",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId1",
                table: "reviews",
                column: "ProfileId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
