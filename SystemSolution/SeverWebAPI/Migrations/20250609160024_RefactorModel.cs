using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefactorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVip",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumEmployees",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVip",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumEmployees",
                table: "AspNetUsers");
        }
    }
}
