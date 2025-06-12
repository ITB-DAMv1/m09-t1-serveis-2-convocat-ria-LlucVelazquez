using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefactorModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "AspNetUsers",
                newName: "DateRegister");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRegister",
                table: "AspNetUsers",
                newName: "RegistrationDate");
        }
    }
}
