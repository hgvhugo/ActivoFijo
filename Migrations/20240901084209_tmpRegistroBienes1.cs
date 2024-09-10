using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class tmpRegistroBienes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tmp_registro_bienes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreBien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEfectos = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstatusId = table.Column<int>(type: "int", nullable: true),
                    FotoBien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartidaId = table.Column<int>(type: "int", nullable: true),
                    CambId = table.Column<int>(type: "int", nullable: true),
                    CucopId = table.Column<int>(type: "int", nullable: true),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorFactura = table.Column<double>(type: "float", nullable: true),
                    ValorDepreciado = table.Column<double>(type: "float", nullable: true),
                    UnidadAdministrativaId = table.Column<int>(type: "int", nullable: true),
                    UbicacionId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    Procesado = table.Column<bool>(type: "bit", nullable: false),
                    Exito = table.Column<bool>(type: "bit", nullable: false),
                    ErrorValidacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargaId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmp_registro_bienes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tmp_registro_bienes");
        }
    }
}
