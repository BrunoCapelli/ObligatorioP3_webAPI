using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class unamigra2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_PaisId",
                table: "Ecosistemas",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenazas_EcosistemaMarinoId",
                table: "Amenazas",
                column: "EcosistemaMarinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenazas_Ecosistemas_EcosistemaMarinoId",
                table: "Amenazas",
                column: "EcosistemaMarinoId",
                principalTable: "Ecosistemas",
                principalColumn: "EcosistemaMarinoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ecosistemas_Paises_PaisId",
                table: "Ecosistemas",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "PaisId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenazas_Ecosistemas_EcosistemaMarinoId",
                table: "Amenazas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ecosistemas_Paises_PaisId",
                table: "Ecosistemas");

            migrationBuilder.DropIndex(
                name: "IX_Ecosistemas_PaisId",
                table: "Ecosistemas");

            migrationBuilder.DropIndex(
                name: "IX_Amenazas_EcosistemaMarinoId",
                table: "Amenazas");
        }
    }
}
