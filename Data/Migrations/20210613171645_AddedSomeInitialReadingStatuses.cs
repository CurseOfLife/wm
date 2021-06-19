using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddedSomeInitialReadingStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3dcc97c7-3313-4e6f-a18b-f4eb490afa88", "1646ec73-cbbb-40a2-beef-3e9378081485", "AndroidUser", "ANDROIDUSER" },
                    { "f5a55056-0565-4509-ac17-a283aaafd883", "31c47c02-1876-4ba1-9570-f150e922acc9", "WebUser", "WEBUSER" },
                    { "48af7be5-af13-40fd-9880-b7e1b607d782", "8f4d6575-6b19-476e-a66c-7b762a609b5a", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "ReadingStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Uspesno" },
                    { 2, "Zakljucana santa" },
                    { 3, "Prljavo brojilo" },
                    { 4, "Auto na santu" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dcc97c7-3313-4e6f-a18b-f4eb490afa88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48af7be5-af13-40fd-9880-b7e1b607d782");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5a55056-0565-4509-ac17-a283aaafd883");

            migrationBuilder.DeleteData(
                table: "ReadingStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReadingStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReadingStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReadingStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12ca4892-16e7-4080-9930-72ed44dc10ec", "6840ed40-c77a-4d52-bf9f-ba29ee28a029", "AndroidUser", "ANDROIDUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "507a051c-b7fe-4d19-9499-31863c348231", "c3c8bc45-f9d9-4797-9a09-0b0b88b010a8", "WebUser", "WEBUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ae61c0a-29d2-4b3e-ac97-9c25457ea299", "254e45bb-14e2-40cf-b4ce-0f5df8c7ca1c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
