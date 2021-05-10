using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMileage.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Mpg");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Mpg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    TempPassword = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "Mpg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fillup",
                schema: "Mpg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Gallons = table.Column<decimal>(type: "decimal(7,3)", nullable: false),
                    Odometer = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    TripOdometer = table.Column<decimal>(type: "decimal(6,1)", nullable: false),
                    DaysSinceLastFillup = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GallonsPerDay = table.Column<decimal>(type: "decimal(6,2)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [Gallons] / [DaysSinceLastFillup], [Gallons] )"),
                    MilesPerDay = table.Column<decimal>(type: "decimal(5,1)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )"),
                    MilesPerGallon = table.Column<decimal>(type: "decimal(5,2)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )"),
                    PricePerDay = table.Column<decimal>(type: "decimal(6,2)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )"),
                    PricePerGallon = table.Column<decimal>(type: "decimal(5,3)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )"),
                    PricePerMile = table.Column<decimal>(type: "decimal(4,2)", nullable: false, computedColumnSql: "iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )"),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fillup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fillup_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "Mpg",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "User",
                columns: new[] { "Id", "IsAdmin", "Password", "TempPassword", "UserName" },
                values: new object[] { 1, true, "F1E11932AD24C091A7F52C56296AE5137DF23BCE6E81306D6BE9343CB1C81F68DC967B80490A4E1178273B4A460A6559BF7ACFEFBCD2A292D599869DB28E89CD", null, "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD" });

            migrationBuilder.CreateIndex(
                name: "IX_Fillup_VehicleId",
                schema: "Mpg",
                table: "Fillup",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "Mpg",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fillup",
                schema: "Mpg");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Mpg");

            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "Mpg");
        }
    }
}
