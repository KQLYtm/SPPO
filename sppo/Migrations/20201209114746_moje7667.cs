using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class moje7667 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ProfileId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ProfileId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "GiverId",
                table: "reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReciverId",
                table: "reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_GiverId",
                table: "reviews",
                column: "GiverId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ReciverId",
                table: "reviews",
                column: "ReciverId");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_GiverId",
                table: "reviews",
                column: "GiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_ReciverId",
                table: "reviews",
                column: "ReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_GiverId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ReciverId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_GiverId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ReciverId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "GiverId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "ReciverId",
                table: "reviews");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "reviews",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
