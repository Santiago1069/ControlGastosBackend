using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlGastosBackend.Migrations
{
    /// <inheritdoc />
    public partial class RegistroGasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosGasto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FondoMonetarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NombreComercio = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TipoDocumento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosGasto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosGasto_FondosMonetarios_FondoMonetarioId",
                        column: x => x.FondoMonetarioId,
                        principalTable: "FondosMonetarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosGasto_FondoMonetarioId",
                table: "RegistrosGasto",
                column: "FondoMonetarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosGasto");
        }
    }
}
