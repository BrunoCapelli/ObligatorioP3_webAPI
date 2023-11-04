using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class unamigra6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenazas_Ecosistemas_EcosistemaMarinoId",
                table: "Amenazas");

            migrationBuilder.DropIndex(
                name: "IX_Amenazas_EcosistemaMarinoId",
                table: "Amenazas");

            migrationBuilder.CreateTable(
                name: "EcosistemaAmenaza",
                columns: table => new
                {
                    EcosistemaMarinoId = table.Column<int>(type: "int", nullable: false),
                    AmenazaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosistemaAmenaza", x => new { x.AmenazaId, x.EcosistemaMarinoId });
                    table.ForeignKey(
                        name: "FK_EcosistemaAmenaza_Amenazas_AmenazaId",
                        column: x => x.AmenazaId,
                        principalTable: "Amenazas",
                        principalColumn: "AmenazaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcosistemaAmenaza_Ecosistemas_EcosistemaMarinoId",
                        column: x => x.EcosistemaMarinoId,
                        principalTable: "Ecosistemas",
                        principalColumn: "EcosistemaMarinoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspecieAmenaza",
                columns: table => new
                {
                    EspecieId = table.Column<int>(type: "int", nullable: false),
                    AmenazaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecieAmenaza", x => new { x.EspecieId, x.AmenazaId });
                    table.ForeignKey(
                        name: "FK_EspecieAmenaza_Amenazas_AmenazaId",
                        column: x => x.AmenazaId,
                        principalTable: "Amenazas",
                        principalColumn: "AmenazaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecieAmenaza_Especies_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especies",
                        principalColumn: "EspecieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcosistemaAmenaza_EcosistemaMarinoId",
                table: "EcosistemaAmenaza",
                column: "EcosistemaMarinoId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecieAmenaza_AmenazaId",
                table: "EspecieAmenaza",
                column: "AmenazaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcosistemaAmenaza");

            migrationBuilder.DropTable(
                name: "EspecieAmenaza");

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
        }
    }
}
