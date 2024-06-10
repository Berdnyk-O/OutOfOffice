using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Migrations
{
    /// <inheritdoc />
    public partial class NotRequiredFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
