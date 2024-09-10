using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class agregaestadobienregistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoFisicoId",
                table: "tbl_registro_bienes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoFisicoId",
                table: "tbl_registro_bienes");
        }
    }
}
