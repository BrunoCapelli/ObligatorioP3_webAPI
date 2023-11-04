using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class addrangeEstadoConservacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "EstadosConservacion",
                newName: "ValorHasta");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "EstadosConservacion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ValorDesde",
                table: "EstadosConservacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EstadosConservacion_Nombre",
                table: "EstadosConservacion",
                column: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EstadosConservacion_Nombre",
                table: "EstadosConservacion");

            migrationBuilder.DropColumn(
                name: "ValorDesde",
                table: "EstadosConservacion");

            migrationBuilder.RenameColumn(
                name: "ValorHasta",
                table: "EstadosConservacion",
                newName: "Valor");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "EstadosConservacion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
