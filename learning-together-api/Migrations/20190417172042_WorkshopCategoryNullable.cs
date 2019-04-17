namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class WorkshopCategoryNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "workshop",
                table: "workshops",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workshops_CategoryId",
                schema: "workshop",
                table: "workshops",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_workshops_category_CategoryId",
                schema: "workshop",
                table: "workshops",
                column: "CategoryId",
                principalSchema: "admin",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workshops_category_CategoryId",
                schema: "workshop",
                table: "workshops");

            migrationBuilder.DropIndex(
                name: "IX_workshops_CategoryId",
                schema: "workshop",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "workshop",
                table: "workshops");
        }
    }
}