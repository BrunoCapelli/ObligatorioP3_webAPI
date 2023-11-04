using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class dropEcosistemaIdcolumnfromAmenaza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EcosistemaMarinoId",
                table: "Amenazas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EcosistemaMarinoId",
                table: "Amenazas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
