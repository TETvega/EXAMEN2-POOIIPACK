using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen2Poo.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "amortitation",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NCuota = table.Column<int>(type: "int", nullable: false),
                    tasa_interes = table.Column<double>(type: "float", nullable: false),
                    abono = table.Column<double>(type: "float", nullable: false),
                    seguro = table.Column<double>(type: "float", nullable: false),
                    cuota_sin_Seguro = table.Column<double>(type: "float", nullable: false),
                    abono_extraordinario = table.Column<double>(type: "float", nullable: false),
                    cuota_con_seguro = table.Column<double>(type: "float", nullable: false),
                    SaldoPrincipal = table.Column<double>(name: "Saldo Principal", type: "float", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amortitation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    identidad = table.Column<int>(type: "int", maxLength: 13, nullable: false),
                    monto_desembolsado = table.Column<double>(type: "float", nullable: false),
                    tasa_comision = table.Column<double>(type: "float", nullable: false),
                    tasa_interes = table.Column<double>(type: "float", nullable: false),
                    termino_pagos = table.Column<int>(type: "int", nullable: false),
                    fecha_desembolso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechadelPrimerPago = table.Column<DateTime>(name: "Fecha del Primer Pago", type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client_amortitation",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    client_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amortitation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_amortitation", x => x.id);
                    table.ForeignKey(
                        name: "FK_client_amortitation_amortitation_amortitation_id",
                        column: x => x.amortitation_id,
                        principalSchema: "dbo",
                        principalTable: "amortitation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_client_amortitation_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "dbo",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_amortitation_amortitation_id",
                schema: "dbo",
                table: "client_amortitation",
                column: "amortitation_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_amortitation_client_id",
                schema: "dbo",
                table: "client_amortitation",
                column: "client_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_amortitation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "amortitation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "client",
                schema: "dbo");
        }
    }
}
