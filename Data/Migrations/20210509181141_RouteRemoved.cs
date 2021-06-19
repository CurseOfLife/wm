using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RouteRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Routes_RouteId",
                table: "MeasuringPoints");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringPoints_RouteId",
                table: "MeasuringPoints");

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
                name: "RouteId",
                table: "MeasuringPoints");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MeasuringPoints",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12ca4892-16e7-4080-9930-72ed44dc10ec", "6840ed40-c77a-4d52-bf9f-ba29ee28a029", "AndroidUser", "ANDROIDUSER" },
                    { "507a051c-b7fe-4d19-9499-31863c348231", "c3c8bc45-f9d9-4797-9a09-0b0b88b010a8", "WebUser", "WEBUSER" },
                    { "6ae61c0a-29d2-4b3e-ac97-9c25457ea299", "254e45bb-14e2-40cf-b4ce-0f5df8c7ca1c", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "f3777d4d-0840-4b53-b603-6695d1cc13b0");

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "f3777d4d-0840-4b53-b603-6695d1cc13b0");

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "f3777d4d-0840-4b53-b603-6695d1cc13b0");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringPoints_UserId",
                table: "MeasuringPoints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_AspNetUsers_UserId",
                table: "MeasuringPoints",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_AspNetUsers_UserId",
                table: "MeasuringPoints");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringPoints_UserId",
                table: "MeasuringPoints");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12ca4892-16e7-4080-9930-72ed44dc10ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "507a051c-b7fe-4d19-9499-31863c348231");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ae61c0a-29d2-4b3e-ac97-9c25457ea299");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MeasuringPoints");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "MeasuringPoints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_MeasuringPoints_RouteId",
                table: "MeasuringPoints",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserId",
                table: "Routes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Routes_RouteId",
                table: "MeasuringPoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
