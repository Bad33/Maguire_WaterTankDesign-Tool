using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTankTool_WFA.Migrations
{
    /// <inheritdoc />
    public partial class tankPropertiesapplied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TankProperties",
                columns: table => new
                {
                    TankNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacity = table.Column<string>(type: "TEXT", nullable: false),
                    WeightOfWater = table.Column<string>(type: "TEXT", nullable: false),
                    WeightOfSteel = table.Column<string>(type: "TEXT", nullable: false),
                    TotalWeight = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectedArea = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankProperties", x => x.TankNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TankProperties");
        }
    }
}
