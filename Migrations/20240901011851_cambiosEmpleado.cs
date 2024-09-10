using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivoFijo.Migrations
{
    /// <inheritdoc />
    public partial class cambiosEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_empleados_cat_ubicaciones_UbicacionId",
                table: "tbl_empleados");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_UbicacionId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_empleados_UbicacionId",
                table: "tbl_empleados");

            migrationBuilder.AlterColumn<int>(
                name: "UbicacionId",
                table: "tbl_empleados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rfc",
                table: "tbl_empleados",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_empleados",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UbicacionId",
                table: "tbl_registro_bienes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_empleados_Email",
                table: "tbl_empleados",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_empleados_Rfc",
                table: "tbl_empleados",
                column: "Rfc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_empleados_UbicacionId",
                table: "tbl_empleados",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_empleados_cat_ubicaciones_UbicacionId",
                table: "tbl_empleados",
                column: "UbicacionId",
                principalTable: "cat_ubicaciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_empleados_cat_ubicaciones_UbicacionId",
                table: "tbl_empleados");

            migrationBuilder.DropIndex(
                name: "IX_tbl_registro_bienes_UbicacionId",
                table: "tbl_registro_bienes");

            migrationBuilder.DropIndex(
                name: "IX_tbl_empleados_Email",
                table: "tbl_empleados");

            migrationBuilder.DropIndex(
                name: "IX_tbl_empleados_Rfc",
                table: "tbl_empleados");

            migrationBuilder.DropIndex(
                name: "IX_tbl_empleados_UbicacionId",
                table: "tbl_empleados");

            migrationBuilder.AlterColumn<int>(
                name: "UbicacionId",
                table: "tbl_empleados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rfc",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "tbl_empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registro_bienes_UbicacionId",
                table: "tbl_registro_bienes",
                column: "UbicacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_empleados_UbicacionId",
                table: "tbl_empleados",
                column: "UbicacionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_empleados_cat_ubicaciones_UbicacionId",
                table: "tbl_empleados",
                column: "UbicacionId",
                principalTable: "cat_ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
