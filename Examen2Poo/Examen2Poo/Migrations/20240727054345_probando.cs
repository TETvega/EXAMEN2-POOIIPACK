using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen2Poo.Migrations
{
    /// <inheritdoc />
    public partial class probando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "identidad_cliente",
                schema: "dbo",
                table: "amortitation",
                newName: "clave_amortizacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clave_amortizacion",
                schema: "dbo",
                table: "amortitation",
                newName: "identidad_cliente");
        }
    }
}
