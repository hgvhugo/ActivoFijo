using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_cambs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_cambs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_cucop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_cucop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_partidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_partidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_unidades_administrativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUnidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_unidades_administrativas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_empleados_cat_ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "cat_ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_registro_bienes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBien = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NombreBien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaEfectos = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstatusId = table.Column<int>(type: "int", nullable: false),
                    FotoBien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PartidaId = table.Column<int>(type: "int", nullable: false),
                    CambId = table.Column<int>(type: "int", nullable: false),
                    CucopId = table.Column<int>(type: "int", nullable: false),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorFactura = table.Column<double>(type: "float", nullable: false),
                    ValorDepreciado = table.Column<double>(type: "float", nullable: false),
                    UnidadAdministrativaId = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    FotosId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_registro_bienes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_cat_cambs_CambId",
                        column: x => x.CambId,
                        principalTable: "cat_cambs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_cat_cucop_CucopId",
                        column: x => x.CucopId,
                        principalTable: "cat_cucop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_cat_partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "cat_partidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_cat_ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "cat_ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_cat_unidades_administrativas_UnidadAdministrativaId",
                        column: x => x.UnidadAdministrativaId,
                        principalTable: "cat_unidades_administrativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_registro_bienes_tbl_empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "tbl_empleados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbr_empleado_unidad_administrativa",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    UnidadAdministrativaId = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModifica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbr_empleado_unidad_administrativa", x => new { x.EmpleadoId, x.UnidadAdministrativaId });
                    table.ForeignKey(
                        name: "FK_tbr_empleado_unidad_administrativa_cat_unidades_administrativas_UnidadAdministrativaId",
                        column: x => x.UnidadAdministrativaId,
                        principalTable: "cat_unidades_administrativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbr_empleado_unidad_administrativa_tbl_empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "tbl_empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_fotos_bien_activo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistroBienesId = table.Column<int>(type: "int", nullable: false),
                    FotoBien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoFotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_fotos_bien_activo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_fotos_bien_activo_tbl_registro_bienes_RegistroBienesId",
                        column: x => x.RegistroBienesId,
                        principalTable: "tbl_registro_bienes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_empleados_UbicacionId",
                table: "tbl_empleados",
                column: "UbicacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_fotos_bien_activo_RegistroBienesId",
                table: "tbl_fotos_bien_activo",
                column: "RegistroBienesId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CambId",
                table: "tbl_registro_bienes",
                column: "CambId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CucopId",
                table: "tbl_registro_bienes",
                column: "CucopId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_EmpleadoId",
                table: "tbl_registro_bienes",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_PartidaId",
                table: "tbl_registro_bienes",
                column: "PartidaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UbicacionId",
                table: "tbl_registro_bienes",
                column: "UbicacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbr_empleado_unidad_administrativa_UnidadAdministrativaId",
                table: "tbr_empleado_unidad_administrativa",
                column: "UnidadAdministrativaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_contratos");

            migrationBuilder.DropTable(
                name: "tbl_fotos_bien_activo");

            migrationBuilder.DropTable(
                name: "tbr_empleado_unidad_administrativa");

            migrationBuilder.DropTable(
                name: "tbl_registro_bienes");

            migrationBuilder.DropTable(
                name: "cat_cambs");

            migrationBuilder.DropTable(
                name: "cat_cucop");

            migrationBuilder.DropTable(
                name: "cat_partidas");

            migrationBuilder.DropTable(
                name: "cat_unidades_administrativas");

            migrationBuilder.DropTable(
                name: "tbl_empleados");

            migrationBuilder.DropTable(
                name: "cat_ubicaciones");
        }
    }
}
