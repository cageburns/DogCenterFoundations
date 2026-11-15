using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogCenterFoundations.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameServiceNotesToDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Services",
                newName: "Description");

            migrationBuilder.Sql(
                "UPDATE [Services] SET [Description] = N'' WHERE [Description] IS NULL");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Services",
                newName: "Notes");
        }
    }
}
