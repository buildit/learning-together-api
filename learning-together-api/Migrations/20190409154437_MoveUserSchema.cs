namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MoveUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_Users",
                "Users");

            migrationBuilder.EnsureSchema(
                "admin");

            migrationBuilder.RenameTable(
                "Users",
                newName: "users",
                newSchema: "admin");

            migrationBuilder.AddPrimaryKey(
                "PK_users",
                schema: "admin",
                table: "users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                "PK_users",
                schema: "admin",
                table: "users");

            migrationBuilder.RenameTable(
                "users",
                "admin",
                "Users");

            migrationBuilder.AddPrimaryKey(
                "PK_Users",
                "Users",
                "Id");
        }
    }
}