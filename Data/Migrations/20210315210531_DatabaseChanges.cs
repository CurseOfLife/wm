using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DatabaseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_WaterMeters_MeasuringPointId",
                table: "WaterMeters");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "MeasuringPoints");

            migrationBuilder.RenameColumn(
                name: "MeasuringPointId",
                table: "Measurements",
                newName: "WaterMeterId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_MeasuringPointId",
                table: "Measurements",
                newName: "IX_Measurements_WaterMeterId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "WaterMeters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "MeasuringPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(95)",
                oldMaxLength: 95,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "MeasuringPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "MeasuringPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Name Surname One");

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Place" },
                values: new object[] { "Name Surname Three", "Test Place Name Three" });

            migrationBuilder.UpdateData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Code",
                value: "123");

            migrationBuilder.UpdateData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "MeasuringPointId" },
                values: new object[] { "234", 2 });

            migrationBuilder.UpdateData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "345");

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_MeasuringPointId",
                table: "WaterMeters",
                column: "MeasuringPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_WaterMeters_WaterMeterId",
                table: "Measurements",
                column: "WaterMeterId",
                principalTable: "WaterMeters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_WaterMeters_WaterMeterId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_WaterMeters_MeasuringPointId",
                table: "WaterMeters");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "WaterMeters");

            migrationBuilder.RenameColumn(
                name: "WaterMeterId",
                table: "Measurements",
                newName: "MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_WaterMeterId",
                table: "Measurements",
                newName: "IX_Measurements_MeasuringPointId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "MeasuringPoints",
                type: "nvarchar(95)",
                maxLength: 95,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "MeasuringPoints",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "MeasuringPoints",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "MeasuringPoints",
                type: "int",
                maxLength: 64,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Place" },
                values: new object[] { null, "Test Place Name One" });

            migrationBuilder.UpdateData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 2,
                column: "MeasuringPointId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_WaterMeters_MeasuringPointId",
                table: "WaterMeters",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
