using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusiOcitavanja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusiOcitavanja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MernaMesta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MernaMesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MernaMesta_Rute_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Rute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brojila",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    StartingValue = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MeasuringPointId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brojila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brojila_MernaMesta_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MernaMesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merenja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasuringPointId = table.Column<int>(type: "int", nullable: false),
                    ReadingStatusId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merenja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merenja_MernaMesta_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MernaMesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Merenja_StatusiOcitavanja_ReadingStatusId",
                        column: x => x.ReadingStatusId,
                        principalTable: "StatusiOcitavanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brojila_MeasuringPointId",
                table: "Brojila",
                column: "MeasuringPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merenja_MeasuringPointId",
                table: "Merenja",
                column: "MeasuringPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Merenja_ReadingStatusId",
                table: "Merenja",
                column: "ReadingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MernaMesta_RouteId",
                table: "MernaMesta",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brojila");

            migrationBuilder.DropTable(
                name: "Merenja");

            migrationBuilder.DropTable(
                name: "MernaMesta");

            migrationBuilder.DropTable(
                name: "StatusiOcitavanja");

            migrationBuilder.DropTable(
                name: "Rute");
        }
    }
}
