using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingOnline.DTO.Migrations
{
    /// <inheritdoc />
    public partial class SetDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
