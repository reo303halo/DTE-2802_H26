using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerApi.Migrations
{
    /// <inheritdoc />
    public partial class CustomUserOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Computers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OwnerId",
                table: "Computers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_AspNetUsers_OwnerId",
                table: "Computers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_AspNetUsers_OwnerId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_OwnerId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Computers");
        }
    }
}
