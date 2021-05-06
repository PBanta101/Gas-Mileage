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
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gallons = table.Column<decimal>(type: "decimal(7,3)", nullable: false),
                    Odometer = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    TripOdometer = table.Column<decimal>(type: "decimal(7,1)", nullable: false),
                    DaysSinceLastFillup = table.Column<int>(type: "int", nullable: false),
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
                table: "Vehicle",
                columns: new[] { "Id", "Color", "Make", "Model", "UserId", "Vin", "Year" },
                values: new object[] { 1, "White", "Toyota", "Pickup", 1, "JT4RN...", 1985 });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "Vehicle",
                columns: new[] { "Id", "Color", "Make", "Model", "UserId", "Vin", "Year" },
                values: new object[] { 2, "Gold", "Saturn", "SW2", 0, null, 1995 });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "Fillup",
                columns: new[] { "Id", "Date", "DaysSinceLastFillup", "Gallons", "Odometer", "TotalCost", "TripOdometer", "VehicleId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 10.234m, 12345, 25.52m, 275.6m, 1 });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "Fillup",
                columns: new[] { "Id", "Date", "DaysSinceLastFillup", "Gallons", "Odometer", "TotalCost", "TripOdometer", "VehicleId" },
                values: new object[] { 2, new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 10.234m, 12700, 25.52m, 275.6m, 1 });

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
