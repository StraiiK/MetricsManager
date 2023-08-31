using Microsoft.EntityFrameworkCore.Migrations;

namespace MetricsAgent.Migrations
{
    public partial class BaseMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Times",
                table: "RomMetrics");

            migrationBuilder.DropColumn(
                name: "Times",
                table: "RamMetrics");

            migrationBuilder.DropColumn(
                name: "Times",
                table: "NetworkMetrics");

            migrationBuilder.DropColumn(
                name: "Times",
                table: "DotNetMetrics");

            migrationBuilder.DropColumn(
                name: "Times",
                table: "CpuMetrics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Times",
                table: "RomMetrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Times",
                table: "RamMetrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Times",
                table: "NetworkMetrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Times",
                table: "DotNetMetrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Times",
                table: "CpuMetrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
