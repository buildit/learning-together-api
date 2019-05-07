namespace learningtogetherapi.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SeedCategoriesLocationsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "admin",
                table: "category",
                columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {1, "Professional Development"},
                    {2, "Emotional Intelligence"},
                    {3, "Teamwork"},
                    {4, "Leadership"},
                    {5, "Design"},
                    {6, "Analytics"},
                    {7, "Culture"},
                    {8, "Agile / Lean"},
                    {9, "Artificial Intelligence"},
                    {10, "Technology"}
                });

            migrationBuilder.InsertData(
                schema: "admin",
                table: "locations",
                columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {6, "Dallas"},
                    {5, "Denver"},
                    {4, "Dublin"},
                    {1, "Brooklyn"},
                    {2, "London"},
                    {3, "Edinburgh"}
                });

            migrationBuilder.InsertData(
                schema: "admin",
                table: "roles",
                columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {7, "Delivery"},
                    {1, "Creative Technologist"},
                    {2, "Frontend"},
                    {3, "Backend"},
                    {4, "Fullstack"},
                    {5, "Design"},
                    {6, "Product"},
                    {8, "Leadership"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "category",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "locations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "roles",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}