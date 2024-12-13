using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTankTool_WFA.Migrations
{
    /// <inheritdoc />
    public partial class WindLoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WindLoadEnitity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Exposure = table.Column<string>(type: "TEXT", nullable: false),
                    Kzt = table.Column<double>(type: "REAL", nullable: false),
                    Ke = table.Column<double>(type: "REAL", nullable: false),
                    Kd = table.Column<double>(type: "REAL", nullable: false),
                    G = table.Column<double>(type: "REAL", nullable: false),
                    I = table.Column<double>(type: "REAL", nullable: false),
                    V = table.Column<double>(type: "REAL", nullable: false),
                    Zg = table.Column<double>(type: "REAL", nullable: false),
                    alpha = table.Column<double>(type: "REAL", nullable: false),
                    lambda = table.Column<double>(type: "REAL", nullable: false),
                    Cf = table.Column<double>(type: "REAL", nullable: false),
                    Q = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindLoadEnitity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WindLoadEnitity");
        }
    }
}
