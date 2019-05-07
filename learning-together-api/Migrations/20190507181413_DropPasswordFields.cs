namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DropPasswordFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "PasswordHash",
                schema: "admin",
                table: "users");

            migrationBuilder.DropColumn(
                "PasswordSalt",
                schema: "admin",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                "PasswordHash",
                schema: "admin",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                "PasswordSalt",
                schema: "admin",
                table: "users",
                nullable: true);
        }
    }
}