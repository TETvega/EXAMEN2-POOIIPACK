using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen2Poo.Migrations
{
    /// <inheritdoc />
    public partial class addFechasPAgos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                schema: "dbo",
                table: "amortitation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "dias_mes",
                schema: "dbo",
                table: "amortitation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                schema: "dbo",
                table: "amortitation");

            migrationBuilder.DropColumn(
                name: "dias_mes",
                schema: "dbo",
                table: "amortitation");
        }
    }
}
