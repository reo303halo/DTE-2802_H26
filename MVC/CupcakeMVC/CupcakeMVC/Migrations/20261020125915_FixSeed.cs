using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CupcakeMVC.Migrations
{
    /// <inheritdoc />
    public partial class FixSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cupcakes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cupcakes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cupcakes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cupcakes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "default-id");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "default-id", 0, "f63818fb-3f27-4e1a-b86d-3ecf8bd49b50", "default@example.com", true, false, null, "DEFAULT@EXAMPLE.COM", "DEFAULT@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBPsWpTtC+b439KYE0kAXnbAbQp8kTQRAeH4VcP+x8TZbvYE6tmo4DL3/genn2S4bQ==", null, false, "", false, "default@example.com" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Regular" },
                    { 2, "Vegan" },
                    { 3, "Gluten Free" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bite Size" },
                    { 2, "Small" },
                    { 3, "Medium" },
                    { 4, "Large" },
                    { 5, "Almost Cake Size" }
                });

            migrationBuilder.InsertData(
                table: "Cupcakes",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "OwnerId", "SizeId" },
                values: new object[,]
                {
                    { 1, 1, "Stuffed with fresh strawberries and topped with fluffy whipped cream, these strawberry shortcake cupcakes are simply divine. Plus, the vanilla cupcake batter couldn’t be easier to whip up.", "Strawberry Shortcake", "default-id", 3 },
                    { 2, 1, "Filled with lemon curd and topped with lemon buttercream frosting, these cupcakes are sweet, tangy, and jam-packed with flavor. They taste like lemon drop candies in cupcake form.", "Lemon Cupcakes", "default-id", 3 },
                    { 3, 1, "These cupcakes are the ultimate chocolate and peanut butter dessert. A dollop of peanut butter frosting tops moist chocolate cupcakes with chocolate drizzle and mini peanut butter cups.These cupcakes are the ultimate chocolate and peanut butter dessert. A dollop of peanut butter frosting tops moist chocolate cupcakes with chocolate drizzle and mini peanut butter cups.", "Chocolate Cupcakes with Peanut Butter Frosting", "default-id", 2 },
                    { 4, 2, "Have a penchant for coconut? These cupcakes are made with a soft, fluffy vanilla cake topped with a coconut cream cheese buttercream frosting. Toasted coconut sprinkled on top makes the entire cupcake tastier.", "Coconut Cupcakes", "default-id", 2 }
                });
        }
    }
}
