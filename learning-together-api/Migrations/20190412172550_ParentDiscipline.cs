namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ParentDiscipline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentDisciplineId",
                schema: "admin",
                table: "disciplines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_disciplines_ParentDisciplineId",
                schema: "admin",
                table: "disciplines",
                column: "ParentDisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplines_disciplines_ParentDisciplineId",
                schema: "admin",
                table: "disciplines",
                column: "ParentDisciplineId",
                principalSchema: "admin",
                principalTable: "disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disciplines_disciplines_ParentDisciplineId",
                schema: "admin",
                table: "disciplines");

            migrationBuilder.DropIndex(
                name: "IX_disciplines_ParentDisciplineId",
                schema: "admin",
                table: "disciplines");

            migrationBuilder.DropColumn(
                name: "ParentDisciplineId",
                schema: "admin",
                table: "disciplines");
        }
    }
}