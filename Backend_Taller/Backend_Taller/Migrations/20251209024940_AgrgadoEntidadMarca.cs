using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Taller.Migrations
{
    /// <inheritdoc />
    public partial class AgrgadoEntidadMarca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Vehiculos");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcasId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_MarcaId",
                table: "Vehiculos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Marcas_MarcaId",
                table: "Vehiculos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcasId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Marcas_MarcaId",
                table: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_MarcaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Vehiculos");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Vehiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
