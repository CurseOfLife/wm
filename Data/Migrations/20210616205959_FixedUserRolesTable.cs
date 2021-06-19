using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FixedUserRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5190757f-76e4-46c9-869f-57c608af28c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfd5f906-a087-46bd-ab89-cdcdf4bfdd50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f248657f-568f-486d-9d65-8c631bca0e02");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85a27a2f-7c11-4379-b4c0-46d565c8d8bb", "8fcde353-7b8f-43bb-9f7e-a67082275e20", "AndroidUser", "ANDROIDUSER" },
                    { "601e7032-7897-47e3-acb1-17ba2310d5a8", "dd4afa86-74fd-4889-b385-79c571cf73e5", "WebUser", "WEBUSER" },
                    { "a7345247-a41e-4947-80a6-26378ca50e25", "8ebfb832-2fe2-4dd0-9dd7-4477b68de4a1", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "f94d651e-3e4a-429d-b987-fa0b9f7be9ea");

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "f94d651e-3e4a-429d-b987-fa0b9f7be9ea");

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "f94d651e-3e4a-429d-b987-fa0b9f7be9ea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "601e7032-7897-47e3-acb1-17ba2310d5a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a27a2f-7c11-4379-b4c0-46d565c8d8bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7345247-a41e-4947-80a6-26378ca50e25");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5190757f-76e4-46c9-869f-57c608af28c1", "3e1dafc8-8d1b-4cec-8a03-ff70acf7f014", "AndroidUser", "ANDROIDUSER" },
                    { "dfd5f906-a087-46bd-ab89-cdcdf4bfdd50", "44f4586b-5321-46ef-84bb-4eb1b7ce8261", "WebUser", "WEBUSER" },
                    { "f248657f-568f-486d-9d65-8c631bca0e02", "0ec02a83-3489-4b7d-bbe6-90e93b5133ee", "Administrator", "ADMINISTRATOR" }
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
        }
    }
}
