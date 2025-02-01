using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiStockMarket.Migrations
{
    /// <inheritdoc />
    public partial class commentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "590a2c46-b386-47a7-bc76-f99b8abea63c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd566972-47b3-49c0-aa09-d3fa638dc81a");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "comments",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a70e5d31-75db-4e8a-9ad0-9a4d2a709d76", null, "Admin", "ADMIN" },
                    { "d1bc5a05-22c1-49e0-a46e-6bedd5cbeaf3", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_AppUserId",
                table: "comments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_AppUserId",
                table: "comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_AppUserId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_AppUserId",
                table: "comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a70e5d31-75db-4e8a-9ad0-9a4d2a709d76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1bc5a05-22c1-49e0-a46e-6bedd5cbeaf3");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "590a2c46-b386-47a7-bc76-f99b8abea63c", null, "User", "USER" },
                    { "dd566972-47b3-49c0-aa09-d3fa638dc81a", null, "Admin", "ADMIN" }
                });
        }
    }
}
