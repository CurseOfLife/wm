using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RouteUserRelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Routes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac955987-5f73-4dcc-9c0a-19d0c9a8833d", "9efca380-e591-493b-bd3f-645bf7a625e4", "AndroidUser", "ANDROIDUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07d891da-ae08-454d-8d34-4a443e076e8e", "d1dd8db3-4da4-4450-9977-ee77f5bca886", "WebUser", "WEBUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e53cf919-0297-4865-9e1c-1fd4647d8c66", "895d5d15-8b9c-4266-b4f9-ea76601baa46", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserId",
                table: "Routes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_AspNetUsers_UserId",
                table: "Routes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_AspNetUsers_UserId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_UserId",
                table: "Routes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07d891da-ae08-454d-8d34-4a443e076e8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac955987-5f73-4dcc-9c0a-19d0c9a8833d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e53cf919-0297-4865-9e1c-1fd4647d8c66");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Routes");

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
    }
}
