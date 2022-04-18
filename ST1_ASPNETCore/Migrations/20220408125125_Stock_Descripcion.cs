using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST1_ASPNETCore.Migrations
{
    /// <inheritdoc />
    public partial class Stock_Descripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Stock");
        }
    }
}
