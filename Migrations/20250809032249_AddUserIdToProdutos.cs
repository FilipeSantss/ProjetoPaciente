using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjeto.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Produtos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MovimentacaoEstoque",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovimentacaoEstoque");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clientes");
        }
    }
}
