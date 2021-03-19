using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialRolesIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9d6f1d7-17af-4458-861a-2cf181a98de2", "616a1551-9102-496e-a585-fee242fee4cc", "AndroidUser", "ANDROIDUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2429376e-7cf0-4074-b9fa-21ff74528567", "815cb514-6efa-43d5-a30e-3da75e76092d", "WebUser", "WEBUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31afc134-d34d-4c91-b54a-c846ccbbb2cc", "6dca3599-5e0c-4bbb-afb4-d617fcdfabfc", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2429376e-7cf0-4074-b9fa-21ff74528567");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31afc134-d34d-4c91-b54a-c846ccbbb2cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9d6f1d7-17af-4458-861a-2cf181a98de2");
        }
    }
}
