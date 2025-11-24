using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlGastosBackend.Migrations
{
    /// <inheritdoc />
    public partial class RegistroGastoDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroGastoDetalle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoGastoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegistroGastoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroGastoDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroGastoDetalle_RegistrosGasto_RegistroGastoId",
                        column: x => x.RegistroGastoId,
                        principalTable: "RegistrosGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroGastoDetalle_TiposGasto_TipoGastoId",
                        column: x => x.TipoGastoId,
                        principalTable: "TiposGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroGastoDetalle_RegistroGastoId",
                table: "RegistroGastoDetalle",
                column: "RegistroGastoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroGastoDetalle_TipoGastoId",
                table: "RegistroGastoDetalle",
                column: "TipoGastoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroGastoDetalle");
        }
    }
}
