using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenFabianAhumada.Migrations
{
    public partial class Migracion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Ubicacion_UbicacionId",
                table: "Proveedor");

            migrationBuilder.DropIndex(
                name: "IX_Proveedor_UbicacionId",
                table: "Proveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UbicacionId",
                table: "Proveedor",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Ubicacion_UbicacionId",
                table: "Proveedor",
                column: "UbicacionId",
                principalTable: "Ubicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
