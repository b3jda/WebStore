using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalPriceToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1633b23e-8dc5-4829-83b2-16b23e5a7af8", null, "Admin", "ADMIN" },
                    { "1f9dfd3e-88da-42c2-8338-fcf9d044f700", null, "AdvancedUser", "ADVANCEDUSER" },
                    { "454f03ec-6baf-46d7-a907-84090e26c870", null, "Client", "CLIENT" },
                    { "691925e7-71d5-405c-b96a-a65f2304058b", null, "SimpleUser", "SIMPLEUSER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1633b23e-8dc5-4829-83b2-16b23e5a7af8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f9dfd3e-88da-42c2-8338-fcf9d044f700");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "454f03ec-6baf-46d7-a907-84090e26c870");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "691925e7-71d5-405c-b96a-a65f2304058b");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Products");

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
    }
}
