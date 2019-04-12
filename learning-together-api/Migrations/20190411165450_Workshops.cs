namespace learningtogetherapi.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    public partial class Workshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "workshop");

            migrationBuilder.CreateTable(
                name: "workshops",
                schema: "workshop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    EducatorId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Webex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workshops_users_EducatorId",
                        column: x => x.EducatorId,
                        principalSchema: "admin",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workshops_locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "admin",
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workshopattendees",
                schema: "workshop",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    WorkshopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workshopattendees", x => new { x.UserId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_workshopattendees_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workshopattendees_workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalSchema: "workshop",
                        principalTable: "workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_workshopattendees_WorkshopId",
                schema: "workshop",
                table: "workshopattendees",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_workshops_EducatorId",
                schema: "workshop",
                table: "workshops",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_workshops_LocationId",
                schema: "workshop",
                table: "workshops",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workshopattendees",
                schema: "workshop");

            migrationBuilder.DropTable(
                name: "workshops",
                schema: "workshop");
        }
    }
}