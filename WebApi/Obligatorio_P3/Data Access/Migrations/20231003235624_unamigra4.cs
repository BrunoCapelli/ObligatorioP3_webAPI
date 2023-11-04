using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class unamigra4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecosistemas_EstadosCo_EstadoConservacionId",
                table: "Ecosistemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Especies_EstadosCo_EstadoConservacionId",
                table: "Especies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosCo",
                table: "EstadosCo");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "EstadosCo",
                newName: "EstadosConservacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosConservacion",
                table: "EstadosConservacion",
                column: "EstadoConservacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecosistemas_EstadosConservacion_EstadoConservacionId",
                table: "Ecosistemas",
                column: "EstadoConservacionId",
                principalTable: "EstadosConservacion",
                principalColumn: "EstadoConservacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Especies_EstadosConservacion_EstadoConservacionId",
                table: "Especies",
                column: "EstadoConservacionId",
                principalTable: "EstadosConservacion",
                principalColumn: "EstadoConservacionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ecosistemas_EstadosConservacion_EstadoConservacionId",
                table: "Ecosistemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Especies_EstadosConservacion_EstadoConservacionId",
                table: "Especies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosConservacion",
                table: "EstadosConservacion");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "EstadosConservacion",
                newName: "EstadosCo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosCo",
                table: "EstadosCo",
                column: "EstadoConservacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ecosistemas_EstadosCo_EstadoConservacionId",
                table: "Ecosistemas",
                column: "EstadoConservacionId",
                principalTable: "EstadosCo",
                principalColumn: "EstadoConservacionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Especies_EstadosCo_EstadoConservacionId",
                table: "Especies",
                column: "EstadoConservacionId",
                principalTable: "EstadosCo",
                principalColumn: "EstadoConservacionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
