using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Data.Migrations
{
    public partial class Weather : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weather",
                table: "Concerts");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Concerts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Forecast",
                table: "Concerts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "Concerts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Concerts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Forecast",
                table: "Concerts");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Concerts");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Concerts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Concerts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weather",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
