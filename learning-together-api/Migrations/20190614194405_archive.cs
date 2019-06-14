using Microsoft.EntityFrameworkCore.Migrations;

namespace learningtogetherapi.Migrations
{
    public partial class archive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArchiveLink",
                schema: "workshop",
                table: "workshops",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveLink",
                schema: "workshop",
                table: "workshops");
        }
    }
}
