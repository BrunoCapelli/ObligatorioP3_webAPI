using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class unamigra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenazas",
                columns: table => new
                {
                    AmenazaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EcosistemaMarinoId = table.Column<int>(type: "int", nullable: false),
                    GradoPeligrosidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenazas", x => x.AmenazaId);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEntidad = table.Column<int>(type: "int", nullable: false),
                    TipoEntidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCo",
                columns: table => new
                {
                    EstadoConservacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCo", x => x.EstadoConservacionId);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "UbisGeograficas",
                columns: table => new
                {
                    UbiGeograficaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false),
                    GradoPeligro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbisGeograficas", x => x.UbiGeograficaId);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Ecosistemas",
                columns: table => new
                {
                    EcosistemaMarinoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UbicacionGeograficaUbiGeograficaId = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    EstadoConservacionId = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    EspecieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosistemas", x => x.EcosistemaMarinoId);
                    table.ForeignKey(
                        name: "FK_Ecosistemas_EstadosCo_EstadoConservacionId",
                        column: x => x.EstadoConservacionId,
                        principalTable: "EstadosCo",
                        principalColumn: "EstadoConservacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ecosistemas_UbisGeograficas_UbicacionGeograficaUbiGeograficaId",
                        column: x => x.UbicacionGeograficaUbiGeograficaId,
                        principalTable: "UbisGeograficas",
                        principalColumn: "UbiGeograficaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especies",
                columns: table => new
                {
                    EspecieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCientifico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVulgar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PesoMin = table.Column<double>(type: "float", nullable: false),
                    PesoMax = table.Column<double>(type: "float", nullable: false),
                    EstadoConservacionId = table.Column<int>(type: "int", nullable: false),
                    EcosistemaMarinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.EspecieId);
                    table.ForeignKey(
                        name: "FK_Especies_Ecosistemas_EcosistemaMarinoId",
                        column: x => x.EcosistemaMarinoId,
                        principalTable: "Ecosistemas",
                        principalColumn: "EcosistemaMarinoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Especies_EstadosCo_EstadoConservacionId",
                        column: x => x.EstadoConservacionId,
                        principalTable: "EstadosCo",
                        principalColumn: "EstadoConservacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_EspecieId",
                table: "Ecosistemas",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_EstadoConservacionId",
                table: "Ecosistemas",
                column: "EstadoConservacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_UbicacionGeograficaUbiGeograficaId",
                table: "Ecosistemas",
                column: "UbicacionGeograficaUbiGeograficaId");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_EcosistemaMarinoId",
                table: "Especies",
                column: "EcosistemaMarinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_EstadoConservacionId",
                table: "Especies",
                column: "EstadoConservacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecosistemas_Especies_EspecieId",
                table: "Ecosistemas",
                column: "EspecieId",
                principalTable: "Especies",
                principalColumn: "EspecieId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecosistemas_Especies_EspecieId",
                table: "Ecosistemas");

            migrationBuilder.DropTable(
                name: "Amenazas");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "Especies");

            migrationBuilder.DropTable(
                name: "Ecosistemas");

            migrationBuilder.DropTable(
                name: "EstadosCo");

            migrationBuilder.DropTable(
                name: "UbisGeograficas");
        }
    }
}
