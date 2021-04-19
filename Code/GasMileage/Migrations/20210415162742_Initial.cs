using Microsoft.EntityFrameworkCore.Migrations;

namespace GasMileage.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Mpg");

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "Mpg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "Mpg");
        }
    }
}
