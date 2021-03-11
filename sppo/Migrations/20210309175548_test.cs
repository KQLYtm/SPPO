using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sppo.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notifications_forms_FormId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_AspNetUsers_ProfileId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_reviews_ReviewId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_FormId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_ProfileId",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "IX_notifications_ReviewId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FromUserId",
                table: "notifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotiBody",
                table: "notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotiHeader",
                table: "notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToUserId",
                table: "notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "NotiBody",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "NotiHeader",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "notifications");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "notifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_FormId",
                table: "notifications",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ProfileId",
                table: "notifications",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ReviewId",
                table: "notifications",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_forms_FormId",
                table: "notifications",
                column: "FormId",
                principalTable: "forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_AspNetUsers_ProfileId",
                table: "notifications",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_reviews_ReviewId",
                table: "notifications",
                column: "ReviewId",
                principalTable: "reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
