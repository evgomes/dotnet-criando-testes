using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CriandoTestes.GestaoUsuarios.Dados.EFCore.Migrations
{
  /// <inheritdoc />
  [ExcludeFromCodeCoverage]
  public partial class Usuarios : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Usuários",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Nome = table.Column<string>(type: "text", nullable: false),
            DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            Email = table.Column<string>(type: "text", nullable: false),
            Senha = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Usuários", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Usuários_Email",
          table: "Usuários",
          column: "Email",
          unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Usuários");
    }
  }
}
