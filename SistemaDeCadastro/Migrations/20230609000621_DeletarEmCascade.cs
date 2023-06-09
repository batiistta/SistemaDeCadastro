using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeCadastro.Migrations
{
    public partial class DeletarEmCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "contatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_contatos_UsuarioId",
                table: "contatos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_contatos_usuarios_UsuarioId",
                table: "contatos",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contatos_usuarios_UsuarioId",
                table: "contatos");

            migrationBuilder.DropIndex(
                name: "IX_contatos_UsuarioId",
                table: "contatos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "contatos");
        }
    }
}
