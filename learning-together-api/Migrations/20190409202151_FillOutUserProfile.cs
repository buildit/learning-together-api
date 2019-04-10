namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class FillOutUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "LocationId",
                schema: "admin",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "RoleId",
                schema: "admin",
                table: "users",
                nullable: true);

            migrationBuilder.CreateTable(
                "disciplines",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_disciplines", x => x.Id); });

            migrationBuilder.CreateTable(
                "locations",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_locations", x => x.Id); });

            migrationBuilder.CreateTable(
                "roles",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_roles", x => x.Id); });

            migrationBuilder.CreateTable(
                "userinterests",
                schema: "admin",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    DisciplineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userinterests", x => new { x.UserId, x.DisciplineId });
                    table.ForeignKey(
                        "FK_userinterests_disciplines_DisciplineId",
                        x => x.DisciplineId,
                        principalSchema: "admin",
                        principalTable: "disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_userinterests_users_UserId",
                        x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_users_LocationId",
                schema: "admin",
                table: "users",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                "IX_users_RoleId",
                schema: "admin",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                "IX_userinterests_DisciplineId",
                schema: "admin",
                table: "userinterests",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                "FK_users_locations_LocationId",
                schema: "admin",
                table: "users",
                column: "LocationId",
                principalSchema: "admin",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_users_roles_RoleId",
                schema: "admin",
                table: "users",
                column: "RoleId",
                principalSchema: "admin",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_users_locations_LocationId",
                schema: "admin",
                table: "users");

            migrationBuilder.DropForeignKey(
                "FK_users_roles_RoleId",
                schema: "admin",
                table: "users");

            migrationBuilder.DropTable(
                "locations",
                "admin");

            migrationBuilder.DropTable(
                "roles",
                "admin");

            migrationBuilder.DropTable(
                "userinterests",
                "admin");

            migrationBuilder.DropTable(
                "disciplines",
                "admin");

            migrationBuilder.DropIndex(
                "IX_users_LocationId",
                schema: "admin",
                table: "users");

            migrationBuilder.DropIndex(
                "IX_users_RoleId",
                schema: "admin",
                table: "users");

            migrationBuilder.DropColumn(
                "LocationId",
                schema: "admin",
                table: "users");

            migrationBuilder.DropColumn(
                "RoleId",
                schema: "admin",
                table: "users");
        }
    }
}