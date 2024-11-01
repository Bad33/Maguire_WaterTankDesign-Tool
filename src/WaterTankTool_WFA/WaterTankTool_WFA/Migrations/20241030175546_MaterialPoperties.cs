using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTankTool_WFA.Migrations
{
    /// <inheritdoc />
    public partial class MaterialPoperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialProperties",
                columns: table => new
                {
                    MaterialNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MaterialType = table.Column<string>(type: "TEXT", nullable: false),
                    Density = table.Column<int>(type: "INTEGER", nullable: false),
                    ModulusOfElasticity = table.Column<int>(type: "INTEGER", nullable: false),
                    TensileYieldStress = table.Column<int>(type: "INTEGER", nullable: false),
                    TensileUltimateStress = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProperties", x => x.MaterialNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialProperties");
        }
    }
}
