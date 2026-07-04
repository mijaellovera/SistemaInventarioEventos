using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramacioIV.Migrations
{
    /// <inheritdoc />
    public partial class AjustePrestamoSinAreaConDevolucion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "FechaDevolucionEstimada",
                table: "Prestamos");

            migrationBuilder.AddColumn<int>(
                name: "CantidadDevuelta",
                table: "DetallePrestamos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadDevuelta",
                table: "DetallePrestamos");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Prestamos",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDevolucionEstimada",
                table: "Prestamos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
