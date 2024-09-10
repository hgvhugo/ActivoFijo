using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class cargaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fechapeticion",
                table: "tmp_registro_bienes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipaddress",
                table: "tmp_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "tmp_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cargaId",
                table: "tbl_registro_bienes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fechapeticion",
                table: "tmp_registro_bienes");

            migrationBuilder.DropColumn(
                name: "ipaddress",
                table: "tmp_registro_bienes");

            migrationBuilder.DropColumn(
                name: "username",
                table: "tmp_registro_bienes");

            migrationBuilder.DropColumn(
                name: "cargaId",
                table: "tbl_registro_bienes");
        }
    }
}
