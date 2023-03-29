using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingOnline.DTO.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
