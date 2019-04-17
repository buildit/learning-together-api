namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class WorkshopRoomUserActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                schema: "workshop",
                table: "workshops",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deactivated",
                schema: "admin",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                schema: "workshop",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "Deactivated",
                schema: "admin",
                table: "users");
        }
    }
}