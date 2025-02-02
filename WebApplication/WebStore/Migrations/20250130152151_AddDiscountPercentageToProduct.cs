using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPercentageToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercentage",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4022e884-5418-47db-b2b6-b0fa48ffced4", null, "Admin", "ADMIN" },
                    { "4ab3894c-0f6b-404d-b917-0d7583aa8fa7", null, "Client", "CLIENT" },
                    { "a7b1c214-64fd-4ed9-83ab-fe989531cfc6", null, "SimpleUser", "SIMPLEUSER" },
                    { "f8b80926-80a5-4f34-8f3a-e84b6f5d5791", null, "AdvancedUser", "ADVANCEDUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4022e884-5418-47db-b2b6-b0fa48ffced4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ab3894c-0f6b-404d-b917-0d7583aa8fa7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7b1c214-64fd-4ed9-83ab-fe989531cfc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8b80926-80a5-4f34-8f3a-e84b6f5d5791");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Products");

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
    }
}
