using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Taller.Migrations
{
    /// <inheritdoc />
    public partial class modificacionDeNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesServicio_RfcMorales_RfcMoralId",
                table: "OrdenesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Marcas_MarcaId",
                table: "Vehiculos");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Vehiculos",
                newName: "MarcasId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehiculos_MarcaId",
                table: "Vehiculos",
                newName: "IX_Vehiculos_MarcasId");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroSerie",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroMotor",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RfcMoralId",
                table: "OrdenesServicio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "OrdenesServicio",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RfcFisico",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cp",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesServicio_RfcMorales_RfcMoralId",
                table: "OrdenesServicio",
                column: "RfcMoralId",
                principalTable: "RfcMorales",
                principalColumn: "RfcMoralId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Marcas_MarcasId",
                table: "Vehiculos",
                column: "MarcasId",
                principalTable: "Marcas",
                principalColumn: "MarcasId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesServicio_RfcMorales_RfcMoralId",
                table: "OrdenesServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Marcas_MarcasId",
                table: "Vehiculos");

            migrationBuilder.RenameColumn(
                name: "MarcasId",
                table: "Vehiculos",
                newName: "MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehiculos_MarcasId",
                table: "Vehiculos",
                newName: "IX_Vehiculos_MarcaId");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroSerie",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroMotor",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RfcMoralId",
                table: "OrdenesServicio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "OrdenesServicio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RfcFisico",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cp",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesServicio_RfcMorales_RfcMoralId",
                table: "OrdenesServicio",
                column: "RfcMoralId",
                principalTable: "RfcMorales",
                principalColumn: "RfcMoralId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Marcas_MarcaId",
                table: "Vehiculos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcasId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
