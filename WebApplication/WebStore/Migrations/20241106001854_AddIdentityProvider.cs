using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4686caef-5c6b-45e7-8b44-4782a5ba1ab5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58a3b490-2ddf-4816-8ea6-af32fb9c1c64");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2858d9e-fc31-4d96-8060-50b0be6dc84c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cacf54fc-7e8f-4b74-8ae6-1e6c9d7b5abb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f990f10-75e3-4e81-ac96-e7b296ca2625", null, "Admin", "ADMIN" },
                    { "897ca2e9-ee34-41d7-b716-1474f973e9a0", null, "Client", "CLIENT" },
                    { "b7eb1058-2685-411e-92d2-40c7d6b1347a", null, "SimpleUser", "SIMPLEUSER" },
                    { "be8aac50-dc15-40c5-9b14-da7f35b0bc8b", null, "AdvancedUser", "ADVANCEDUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f990f10-75e3-4e81-ac96-e7b296ca2625");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "897ca2e9-ee34-41d7-b716-1474f973e9a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7eb1058-2685-411e-92d2-40c7d6b1347a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be8aac50-dc15-40c5-9b14-da7f35b0bc8b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4686caef-5c6b-45e7-8b44-4782a5ba1ab5", null, "AdvancedUser", "ADVANCEDUSER" },
                    { "58a3b490-2ddf-4816-8ea6-af32fb9c1c64", null, "Admin", "ADMIN" },
                    { "a2858d9e-fc31-4d96-8060-50b0be6dc84c", null, "SimpleUser", "SIMPLEUSER" },
                    { "cacf54fc-7e8f-4b74-8ae6-1e6c9d7b5abb", null, "Client", "CLIENT" }
                });
        }
    }
}
