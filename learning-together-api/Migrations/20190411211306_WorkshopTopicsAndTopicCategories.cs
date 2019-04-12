namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class WorkshopTopicsAndTopicCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "admin",
                table: "disciplines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "category",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_category", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "workshoptopics",
                schema: "workshop",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(nullable: false),
                    DisciplineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshoptopics", x => new { x.WorkshopId, x.DisciplineId });
                    table.ForeignKey(
                        name: "FK_workshoptopics_disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "admin",
                        principalTable: "disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workshoptopics_workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalSchema: "workshop",
                        principalTable: "workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disciplines_CategoryId",
                schema: "admin",
                table: "disciplines",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_workshoptopics_DisciplineId",
                schema: "workshop",
                table: "workshoptopics",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplines_category_CategoryId",
                schema: "admin",
                table: "disciplines",
                column: "CategoryId",
                principalSchema: "admin",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disciplines_category_CategoryId",
                schema: "admin",
                table: "disciplines");

            migrationBuilder.DropTable(
                name: "category",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "workshoptopics",
                schema: "workshop");

            migrationBuilder.DropIndex(
                name: "IX_disciplines_CategoryId",
                schema: "admin",
                table: "disciplines");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "admin",
                table: "disciplines");
        }
    }
}