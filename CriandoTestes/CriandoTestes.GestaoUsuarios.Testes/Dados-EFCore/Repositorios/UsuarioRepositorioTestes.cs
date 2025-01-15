using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using CriandoTestes.GestaoUsuarios.Dados.EFCore.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CriandoTestes.GestaoUsuarios.Tests.Repositorios;

public class UsuarioRepositorioTestes
{
  private readonly DbContextOptions<GestaoUsuariosContexto> _dbContextOptions;

  public UsuarioRepositorioTestes()
  {
    // Provedor de base de dados em memória para testes unitários. Para testes mais assertivos, é mais adequado
    // criar testes de integração apontando para uma base de dados real.
    _dbContextOptions = new DbContextOptionsBuilder<GestaoUsuariosContexto>()
        .UseInMemoryDatabase(databaseName: "BaseTestes")
        .Options;
  }

  [Fact]
  public async Task BuscarPorEmailAsync_DeveRetornarUsuario_QuandoUsuarioExisteAsync()
  {
    // Arrange
    var email = "teste@exemplo.com";
    var usuario = new Usuario("Teste", DateTime.Now, email, "password123");

    await using var context = new GestaoUsuariosContexto(_dbContextOptions);
    await context.Usuarios.AddAsync(usuario);
    await context.SaveChangesAsync();

    var repositorio = new UsuarioRepositorio(context);

    // Act
    var result = await repositorio.BuscarPorEmailAsync(email, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(email, result?.Email);
  }

  [Fact]
  public async Task BuscarPorEmailAsync_DeveRetornarNull_QuandoUsuarioNaoExisteAsync()
  {
    // Arrange
    var email = "naoexistente@exemplo.com";

    await using var context = new GestaoUsuariosContexto(_dbContextOptions);
    var repositorio = new UsuarioRepositorio(context);

    // Act
    var result = await repositorio.BuscarPorEmailAsync(email, CancellationToken.None);

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public async Task CriarAsync_DeveCriarUsuarioAsync()
  {
    // Arrange (usando um provedor de base de dados em memória para testes unitários)
    var contexto = new GestaoUsuariosContexto(_dbContextOptions);
    var repositorio = new UsuarioRepositorio(contexto);

    var usuario = new Usuario("Jhon Doe", new DateTime(1990, 1, 1), "jhondoe@gmail.com", "password123");

    // Act
    await repositorio.CriarAsync(usuario, CancellationToken.None);

    // Assert
    var usuarioFromDb = await contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == "jhondoe@gmail.com");
    Assert.NotNull(usuarioFromDb);
    Assert.Equal("Jhon Doe", usuarioFromDb.Nome);
  }
}
