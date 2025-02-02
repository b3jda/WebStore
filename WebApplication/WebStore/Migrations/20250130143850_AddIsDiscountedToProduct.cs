using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDiscountedToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscounted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00bb4f48-da22-4559-85a4-49d12fa6e935", null, "Client", "CLIENT" },
                    { "32fc5305-ba03-4d18-8881-54352ae38ea3", null, "SimpleUser", "SIMPLEUSER" },
                    { "5915904c-e500-4eca-8037-da8c1f4c9fb2", null, "Admin", "ADMIN" },
                    { "75dfde5d-54cd-400d-bb0f-8ef0f3d65ec1", null, "AdvancedUser", "ADVANCEDUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00bb4f48-da22-4559-85a4-49d12fa6e935");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32fc5305-ba03-4d18-8881-54352ae38ea3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5915904c-e500-4eca-8037-da8c1f4c9fb2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75dfde5d-54cd-400d-bb0f-8ef0f3d65ec1");

            migrationBuilder.DropColumn(
                name: "IsDiscounted",
                table: "Products");

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
    }
}
