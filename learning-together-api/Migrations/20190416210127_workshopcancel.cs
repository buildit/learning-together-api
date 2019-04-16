namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class workshopcancel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                schema: "workshop",
                table: "workshops",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                schema: "workshop",
                table: "workshops");
        }
    }
}