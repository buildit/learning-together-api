namespace learningtogetherapi.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class auditfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "workshop",
                table: "workshops",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "workshop",
                table: "workshops",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "workshop",
                table: "workshopattendees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                schema: "admin",
                table: "users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "workshop",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "workshop",
                table: "workshops");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "workshop",
                table: "workshopattendees");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                schema: "admin",
                table: "users");
        }
    }
}