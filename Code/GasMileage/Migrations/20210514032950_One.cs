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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Gallons = table.Column<decimal>(type: "decimal(7,3)", nullable: false),
                    Odometer = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    TripOdometer = table.Column<decimal>(type: "decimal(6,1)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: true),
                    DaysSinceLastFillup = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GallonsPerDay = table.Column<decimal>(type: "decimal(6,2)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [Gallons] / [DaysSinceLastFillup], [Gallons] )"),
                    MilesPerDay = table.Column<decimal>(type: "decimal(5,1)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TripOdometer] / [DaysSinceLastFillup], [TripOdometer] )"),
                    MilesPerGallon = table.Column<decimal>(type: "decimal(5,2)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TripOdometer] / [Gallons], 999.9 )"),
                    PricePerDay = table.Column<decimal>(type: "decimal(6,2)", nullable: false, computedColumnSql: "iif( [DaysSinceLastFillup] > 1, [TotalCost] / [DaysSinceLastFillup], [TotalCost] )"),
                    PricePerGallon = table.Column<decimal>(type: "decimal(5,3)", nullable: false, computedColumnSql: "iif( [Gallons] > 0, [TotalCost] / [Gallons], 999.9 )"),
                    PricePerMile = table.Column<decimal>(type: "decimal(4,2)", nullable: false, computedColumnSql: "iif( [TripOdometer] > 0, [TotalCost] / [TripOdometer], 999.9 )"),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                values: new object[] { new Guid("469faf7d-507d-456a-5d5c-08d9140bf7c6"), true, "F1E11932AD24C091A7F52C56296AE5137DF23BCE6E81306D6BE9343CB1C81F68DC967B80490A4E1178273B4A460A6559BF7ACFEFBCD2A292D599869DB28E89CD", null, "85FDFD0FB6DFE3AFED031983A1EAEC69ADB8E91CFCEB9FA3EBFAA6984C1E564541CCA57A965FD4C6ACF6632EB0130F42F70E4E52EA038B111B6E16461F2165CD" });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "Vehicle",
                columns: new[] { "Id", "Color", "Make", "Model", "UserId", "Vin", "Year" },
                values: new object[] { new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), "Red", "Subaru", "Forester", new Guid("469faf7d-507d-456a-5d5c-08d9140bf7c6"), null, 2002 });

            migrationBuilder.InsertData(
                schema: "Mpg",
                table: "Fillup",
                columns: new[] { "Id", "Date", "DaysSinceLastFillup", "Gallons", "Odometer", "TotalCost", "TripOdometer", "VehicleId", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("c61c29ad-9be1-4eb3-a9b2-08d91576fae3"), new DateTime(2020, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11.104m, 147360, 21.64m, 225.7m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("44fd61fc-df99-4eee-a9a4-08d91576fae3"), new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12.652m, 150179, 36.17m, 333.7m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("b5949bba-d0d7-4183-a9a5-08d91576fae3"), new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12.192m, 149846, 33.88m, 259m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 69101 },
                    { new Guid("3ba9f92b-9c0a-4ab2-a9a6-08d91576fae3"), new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12.317m, 149587, 34.23m, 308.2m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 68502 },
                    { new Guid("87354378-e1a9-472f-a9a7-08d91576fae3"), new DateTime(2021, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10.138m, 149278, 25.33m, 274.8m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 69153 },
                    { new Guid("510a1007-96e4-4069-a9a8-08d91576fae3"), new DateTime(2021, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4.677m, 149003, 13.23m, 119.8m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("a745ed52-28c9-4951-a9a9-08d91576fae3"), new DateTime(2021, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 10.438m, 148884, 29.53m, 271.3m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("bd3eb530-2d7e-40e3-a9a3-08d91576fae3"), new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8.672m, 150424, 27.31m, 244.9m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), null },
                    { new Guid("5e9a7626-db13-44f7-a9aa-08d91576fae3"), new DateTime(2021, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, 8.099m, 148612, 22.99m, 193.7m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("98f47b0a-02cb-4190-a9ac-08d91576fae3"), new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 38, 7.078m, 148263, 19.53m, 157.8m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("45f6d3a5-dd36-46bb-a9ad-08d91576fae3"), new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 7.408m, 148105, 16.29m, 187.9m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("ff53ce12-e9c3-48bd-a9ae-08d91576fae3"), new DateTime(2021, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5.324m, 147917, 10.91m, 135.1m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("bc2a0ff1-e36f-4ce2-a9af-08d91576fae3"), new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5.134m, 147782, 10.37m, 129m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("25a96d7a-ea59-47c5-a9b0-08d91576fae3"), new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 5.549m, 147653, 11.2m, 141m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("3e6b136b-a2b5-4ed9-a9b1-08d91576fae3"), new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5.881m, 147512, 11.87m, 151.6m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("89c21c3d-acb0-4d57-a9ab-08d91576fae3"), new DateTime(2021, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5.739m, 148418, 16.06m, 155.2m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 },
                    { new Guid("c58885ee-02d6-4bad-34db-08d91576652f"), new DateTime(2021, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7.703m, 150624, 27.31m, 199.8m, new Guid("0def444a-db95-47ec-fa5b-08d91575881a"), 80132 }
                });

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
