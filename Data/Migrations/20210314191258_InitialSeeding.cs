using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brojila_MernaMesta_MeasuringPointId",
                table: "Brojila");

            migrationBuilder.DropForeignKey(
                name: "FK_Merenja_MernaMesta_MeasuringPointId",
                table: "Merenja");

            migrationBuilder.DropForeignKey(
                name: "FK_Merenja_StatusiOcitavanja_ReadingStatusId",
                table: "Merenja");

            migrationBuilder.DropForeignKey(
                name: "FK_MernaMesta_Rute_RouteId",
                table: "MernaMesta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusiOcitavanja",
                table: "StatusiOcitavanja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rute",
                table: "Rute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MernaMesta",
                table: "MernaMesta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merenja",
                table: "Merenja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brojila",
                table: "Brojila");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "MernaMesta");

            migrationBuilder.RenameTable(
                name: "StatusiOcitavanja",
                newName: "ReadingStatuses");

            migrationBuilder.RenameTable(
                name: "Rute",
                newName: "Routes");

            migrationBuilder.RenameTable(
                name: "MernaMesta",
                newName: "MeasuringPoints");

            migrationBuilder.RenameTable(
                name: "Merenja",
                newName: "Measurements");

            migrationBuilder.RenameTable(
                name: "Brojila",
                newName: "WaterMeters");

            migrationBuilder.RenameIndex(
                name: "IX_MernaMesta_RouteId",
                table: "MeasuringPoints",
                newName: "IX_MeasuringPoints_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Merenja_ReadingStatusId",
                table: "Measurements",
                newName: "IX_Measurements_ReadingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Merenja_MeasuringPointId",
                table: "Measurements",
                newName: "IX_Measurements_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_Brojila_MeasuringPointId",
                table: "WaterMeters",
                newName: "IX_WaterMeters_MeasuringPointId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReadingStatuses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "MeasuringPoints",
                type: "nvarchar(95)",
                maxLength: 95,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "MeasuringPoints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "MeasuringPoints",
                type: "int",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingStatuses",
                table: "ReadingStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WaterMeters",
                table: "WaterMeters",
                column: "Id");

            migrationBuilder.InsertData(
                table: "MeasuringPoints",
                columns: new[] { "Id", "Code", "Description", "Number", "Place", "RouteId", "Street" },
                values: new object[] { 1, null, null, "1", "Test Place Name One", null, "Test Street Name One" });

            migrationBuilder.InsertData(
                table: "MeasuringPoints",
                columns: new[] { "Id", "Code", "Description", "Number", "Place", "RouteId", "Street" },
                values: new object[] { 2, null, null, "2", "Test Place Name Two", null, "Test Street Name Two" });

            migrationBuilder.InsertData(
                table: "MeasuringPoints",
                columns: new[] { "Id", "Code", "Description", "Number", "Place", "RouteId", "Street" },
                values: new object[] { 3, null, null, "3", "Test Place Name One", null, "Test Street Name Three" });

            migrationBuilder.InsertData(
                table: "WaterMeters",
                columns: new[] { "Id", "IsActive", "MaxValue", "MeasuringPointId", "StartingValue" },
                values: new object[] { 1, true, 100, 1, null });

            migrationBuilder.InsertData(
                table: "WaterMeters",
                columns: new[] { "Id", "IsActive", "MaxValue", "MeasuringPointId", "StartingValue" },
                values: new object[] { 3, true, 100, 3, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_ReadingStatuses_ReadingStatusId",
                table: "Measurements",
                column: "ReadingStatusId",
                principalTable: "ReadingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringPoints_Routes_RouteId",
                table: "MeasuringPoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterMeters_MeasuringPoints_MeasuringPointId",
                table: "WaterMeters",
                column: "MeasuringPointId",
                principalTable: "MeasuringPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_MeasuringPoints_MeasuringPointId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_ReadingStatuses_ReadingStatusId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringPoints_Routes_RouteId",
                table: "MeasuringPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterMeters_MeasuringPoints_MeasuringPointId",
                table: "WaterMeters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WaterMeters",
                table: "WaterMeters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingStatuses",
                table: "ReadingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasuringPoints",
                table: "MeasuringPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements");

            migrationBuilder.DeleteData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WaterMeters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MeasuringPoints",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "WaterMeters",
                newName: "Brojila");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Rute");

            migrationBuilder.RenameTable(
                name: "ReadingStatuses",
                newName: "StatusiOcitavanja");

            migrationBuilder.RenameTable(
                name: "MeasuringPoints",
                newName: "MernaMesta");

            migrationBuilder.RenameTable(
                name: "Measurements",
                newName: "Merenja");

            migrationBuilder.RenameIndex(
                name: "IX_WaterMeters_MeasuringPointId",
                table: "Brojila",
                newName: "IX_Brojila_MeasuringPointId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasuringPoints_RouteId",
                table: "MernaMesta",
                newName: "IX_MernaMesta_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_ReadingStatusId",
                table: "Merenja",
                newName: "IX_Merenja_ReadingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Measurements_MeasuringPointId",
                table: "Merenja",
                newName: "IX_Merenja_MeasuringPointId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StatusiOcitavanja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "MernaMesta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(95)",
                oldMaxLength: 95,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "MernaMesta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "MernaMesta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "MernaMesta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "MernaMesta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "MernaMesta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brojila",
                table: "Brojila",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rute",
                table: "Rute",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusiOcitavanja",
                table: "StatusiOcitavanja",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MernaMesta",
                table: "MernaMesta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merenja",
                table: "Merenja",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brojila_MernaMesta_MeasuringPointId",
                table: "Brojila",
                column: "MeasuringPointId",
                principalTable: "MernaMesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merenja_MernaMesta_MeasuringPointId",
                table: "Merenja",
                column: "MeasuringPointId",
                principalTable: "MernaMesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merenja_StatusiOcitavanja_ReadingStatusId",
                table: "Merenja",
                column: "ReadingStatusId",
                principalTable: "StatusiOcitavanja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MernaMesta_Rute_RouteId",
                table: "MernaMesta",
                column: "RouteId",
                principalTable: "Rute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
