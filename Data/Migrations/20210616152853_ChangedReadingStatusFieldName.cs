using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangedReadingStatusFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ReadingStatuses",
                newName: "Value");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5190757f-76e4-46c9-869f-57c608af28c1", "3e1dafc8-8d1b-4cec-8a03-ff70acf7f014", "AndroidUser", "ANDROIDUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfd5f906-a087-46bd-ab89-cdcdf4bfdd50", "44f4586b-5321-46ef-84bb-4eb1b7ce8261", "WebUser", "WEBUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f248657f-568f-486d-9d65-8c631bca0e02", "0ec02a83-3489-4b7d-bbe6-90e93b5133ee", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ReadingStatuses",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3dcc97c7-3313-4e6f-a18b-f4eb490afa88", "1646ec73-cbbb-40a2-beef-3e9378081485", "AndroidUser", "ANDROIDUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5a55056-0565-4509-ac17-a283aaafd883", "31c47c02-1876-4ba1-9570-f150e922acc9", "WebUser", "WEBUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48af7be5-af13-40fd-9880-b7e1b607d782", "8f4d6575-6b19-476e-a66c-7b762a609b5a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
