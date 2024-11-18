using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterTankTool_WFA.Migrations
{
    /// <inheritdoc />
    public partial class segmentTableDIFColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiameterFinal",
                table: "SegmentProperties",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiameterInitial",
                table: "SegmentProperties",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiameterFinal",
                table: "SegmentProperties");

            migrationBuilder.DropColumn(
                name: "DiameterInitial",
                table: "SegmentProperties");
        }
    }
}
