namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class WorkshopRobinEventid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RobinEventId",
                schema: "workshop",
                table: "workshops",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RobinEventId",
                schema: "workshop",
                table: "workshops");
        }
    }
}