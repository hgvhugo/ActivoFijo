using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class camposdefirma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CadenaOriginalEmpleado",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CadenaOriginalResponsable",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirmaEmpleado",
                table: "tbl_registro_bienes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFirmaResponsable",
                table: "tbl_registro_bienes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmaEmpleado",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmaResponsable",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadenaOriginalEmpleado",
                table: "tbl_registro_bienes");

            migrationBuilder.DropColumn(
                name: "CadenaOriginalResponsable",
                table: "tbl_registro_bienes");

            migrationBuilder.DropColumn(
                name: "FechaFirmaEmpleado",
                table: "tbl_registro_bienes");

            migrationBuilder.DropColumn(
                name: "FechaFirmaResponsable",
                table: "tbl_registro_bienes");

            migrationBuilder.DropColumn(
                name: "FirmaEmpleado",
                table: "tbl_registro_bienes");

            migrationBuilder.DropColumn(
                name: "FirmaResponsable",
                table: "tbl_registro_bienes");
        }
    }
}
