using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupcakeMVC.Migrations
{
    /// <inheritdoc />
    public partial class FixHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "default-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f63818fb-3f27-4e1a-b86d-3ecf8bd49b50", "AQAAAAIAAYagAAAAEBPsWpTtC+b439KYE0kAXnbAbQp8kTQRAeH4VcP+x8TZbvYE6tmo4DL3/genn2S4bQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "default-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "22106a16-b48b-42ce-8fc2-37023e7df010", "AQAAAAIAAYagAAAAEKzm1cc3PNxoSLkmzxGeSWDSvOGYgjLKA21LASj6/vRvxWTidxheFqsuJsyH7z2WWg==" });
        }
    }
}
