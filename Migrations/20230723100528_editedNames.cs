using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace form.Migrations
{
    /// <inheritdoc />
    public partial class editedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "Employees",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "EmployeeAdress",
                table: "Employees",
                newName: "EmployeeAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Employees",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "EmployeeAddress",
                table: "Employees",
                newName: "EmployeeAdress");
        }
    }
}
