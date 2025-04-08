using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _Net.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShoppingCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "ShoppingCard",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "ShoppingCard");
        }
    }
}
