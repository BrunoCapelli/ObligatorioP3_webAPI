using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class unamigra5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecosistemas_UbisGeograficas_UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas");

            migrationBuilder.DropTable(
                name: "UbisGeograficas");

            migrationBuilder.DropIndex(
                name: "IX_Ecosistemas_UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas");

            migrationBuilder.RenameColumn(
                name: "UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas",
                newName: "UbicacionGeografica_GradoPeligro");

            migrationBuilder.AddColumn<double>(
                name: "UbicacionGeografica_Latitud",
                table: "Ecosistemas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UbicacionGeografica_Longitud",
                table: "Ecosistemas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UbicacionGeografica_Latitud",
                table: "Ecosistemas");

            migrationBuilder.DropColumn(
                name: "UbicacionGeografica_Longitud",
                table: "Ecosistemas");

            migrationBuilder.RenameColumn(
                name: "UbicacionGeografica_GradoPeligro",
                table: "Ecosistemas",
                newName: "UbicacionGeograficaUbiGeograficaId");

            migrationBuilder.CreateTable(
                name: "UbisGeograficas",
                columns: table => new
                {
                    UbiGeograficaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradoPeligro = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbisGeograficas", x => x.UbiGeograficaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas",
                column: "UbicacionGeograficaUbiGeograficaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecosistemas_UbisGeograficas_UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas",
                column: "UbicacionGeograficaUbiGeograficaId",
                principalTable: "UbisGeograficas",
                principalColumn: "UbiGeograficaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
