using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class cambioentablaRegistrobienes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_cambs_CambId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_cucop_CucopId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_partidas_PartidaId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_ubicaciones_UbicacionId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_unidades_administrativas_UnidadAdministrativaId",
                table: "tbl_registro_bienes");

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

            migrationBuilder.AlterColumn<double>(
                name: "ValorFactura",
                table: "tbl_registro_bienes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "ValorDepreciado",
                table: "tbl_registro_bienes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UbicacionId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Serie",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "PartidaId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroFactura",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroContrato",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FotoBien",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFactura",
                table: "tbl_registro_bienes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "tbl_registro_bienes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CucopId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CambId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_cambs_CambId",
                table: "tbl_registro_bienes",
                column: "CambId",
                principalTable: "cat_cambs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_cucop_CucopId",
                table: "tbl_registro_bienes",
                column: "CucopId",
                principalTable: "cat_cucop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_partidas_PartidaId",
                table: "tbl_registro_bienes",
                column: "PartidaId",
                principalTable: "cat_partidas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_ubicaciones_UbicacionId",
                table: "tbl_registro_bienes",
                column: "UbicacionId",
                principalTable: "cat_ubicaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_unidades_administrativas_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId",
                principalTable: "cat_unidades_administrativas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_cambs_CambId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_cucop_CucopId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_partidas_PartidaId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_ubicaciones_UbicacionId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_registro_bienes_cat_unidades_administrativas_UnidadAdministrativaId",
                table: "tbl_registro_bienes");

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

            migrationBuilder.AlterColumn<double>(
                name: "ValorFactura",
                table: "tbl_registro_bienes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ValorDepreciado",
                table: "tbl_registro_bienes",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UbicacionId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Serie",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartidaId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroFactura",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroContrato",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "tbl_registro_bienes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FotoBien",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFactura",
                table: "tbl_registro_bienes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "tbl_registro_bienes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CucopId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CambId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "IX_tbl_registro_bienes_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_cambs_CambId",
                table: "tbl_registro_bienes",
                column: "CambId",
                principalTable: "cat_cambs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_cucop_CucopId",
                table: "tbl_registro_bienes",
                column: "CucopId",
                principalTable: "cat_cucop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_partidas_PartidaId",
                table: "tbl_registro_bienes",
                column: "PartidaId",
                principalTable: "cat_partidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_ubicaciones_UbicacionId",
                table: "tbl_registro_bienes",
                column: "UbicacionId",
                principalTable: "cat_ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_registro_bienes_cat_unidades_administrativas_UnidadAdministrativaId",
                table: "tbl_registro_bienes",
                column: "UnidadAdministrativaId",
                principalTable: "cat_unidades_administrativas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
