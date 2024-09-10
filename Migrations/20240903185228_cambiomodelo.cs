using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class cambiomodelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_CambId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_CucopId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_EmpleadoId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_PartidaId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CambId",
                table: "tbl_registro_bienes",
                column: "CambId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CucopId",
                table: "tbl_registro_bienes",
                column: "CucopId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_EmpleadoId",
                table: "tbl_registro_bienes",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_PartidaId",
                table: "tbl_registro_bienes",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_CambId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_CucopId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_EmpleadoId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_PartidaId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CambId",
                table: "tbl_registro_bienes",
                column: "CambId",
                unique: true,
                filter: "[CambId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_CucopId",
                table: "tbl_registro_bienes",
                column: "CucopId",
                unique: true,
                filter: "[CucopId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_EmpleadoId",
                table: "tbl_registro_bienes",
                column: "EmpleadoId",
                unique: true,
                filter: "[EmpleadoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_PartidaId",
                table: "tbl_registro_bienes",
                column: "PartidaId",
                unique: true,
                filter: "[PartidaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId",
                unique: true,
                filter: "[UnidadAdministrativaId] IS NOT NULL");
        }
    }
}
