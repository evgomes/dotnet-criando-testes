using System.Diagnostics.CodeAnalysis;
using CriandoTestes.GestaoUsuarios.Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;

[ExcludeFromCodeCoverage]
public class GestaoUsuariosContexto : DbContext
{
  public DbSet<Usuario> Usuarios { get; set; }

  public GestaoUsuariosContexto(DbContextOptions<GestaoUsuariosContexto> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Usuario>().ToTable("Usuários");
    modelBuilder.Entity<Usuario>().HasKey(x => x.Id);
    modelBuilder.Entity<Usuario>().HasIndex(x => x.Email).IsUnique(true);

    modelBuilder.Entity<Usuario>().Property(x => x.Nome).IsRequired();
    modelBuilder.Entity<Usuario>().Property(x => x.DataNascimento).IsRequired();
    modelBuilder.Entity<Usuario>().Property(x => x.Email).IsRequired();
    modelBuilder.Entity<Usuario>().Property(x => x.Senha).IsRequired();
  }
}
