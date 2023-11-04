using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class addingUnionEspecieEcosistemaMarino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EcosistemaMarinoEspecie",
                columns: table => new
                {
                    EspecieId = table.Column<int>(type: "int", nullable: false),
                    EcosistemaMarinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosistemaMarinoEspecie", x => new { x.EcosistemaMarinoId, x.EspecieId });
                    table.ForeignKey(
                        name: "FK_EcosistemaMarinoEspecie_Ecosistemas_EcosistemaMarinoId",
                        column: x => x.EcosistemaMarinoId,
                        principalTable: "Ecosistemas",
                        principalColumn: "EcosistemaMarinoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcosistemaMarinoEspecie_Especies_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especies",
                        principalColumn: "EspecieId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcosistemaMarinoEspecie_EspecieId",
                table: "EcosistemaMarinoEspecie",
                column: "EspecieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcosistemaMarinoEspecie");
        }
    }
}
