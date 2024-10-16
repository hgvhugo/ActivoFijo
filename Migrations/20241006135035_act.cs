using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class act : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_registro_bienes_log",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CodigoBien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreBien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEfectos = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstatusId = table.Column<int>(type: "int", nullable: false),
                    FotoBien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoFisicoId = table.Column<int>(type: "int", nullable: false),
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
                    FotosId = table.Column<int>(type: "int", nullable: true),
                    FirmaEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CadenaOriginalEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFirmaEmpleado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirmaResponsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CadenaOriginalResponsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFirmaResponsable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cargaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioModificaLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddressLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaModificacionLog = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_registro_bienes_log", x => x.LogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_registro_bienes_log");
        }
    }
}
