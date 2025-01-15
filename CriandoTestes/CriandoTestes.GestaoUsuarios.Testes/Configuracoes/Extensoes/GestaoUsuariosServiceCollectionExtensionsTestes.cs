using CriandoTestes.GestaoUsuarios.Configuracoes.Extensoes;
using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using CriandoTestes.GestaoUsuarios.Dados.EFCore.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.Servicos.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CriandoTestes.GestaoUsuarios.Testes.Configuracoes.Extensoes;

public class GestaoUsuariosServiceCollectionExtensionsTestes
{
  [Fact]
  public void AddGestaoUsuarios_DeveRegistrarDependencias()
  {
    // Arrange (mock da service collection e configurações)
    var services = new ServiceCollection();
    services.AddLogging();

    var configuration = Substitute.For<IConfiguration>();
    var connectionString = "Server=127.0.0.1;Port=5432;Database=testes;User Id=testes;Password=testes123;";
    configuration.GetConnectionString("GestaoUsuarios").Returns(connectionString);

    // Act
    services.AddGestaoUsuarios(configuration);
    var serviceProvider = services.BuildServiceProvider();

    // Assert
    var dbContext = serviceProvider.GetService<GestaoUsuariosContexto>();
    Assert.NotNull(dbContext);

    var usuarioRepositorio = serviceProvider.GetService<IUsuarioRepositorio>();
    Assert.NotNull(usuarioRepositorio);
    Assert.IsType<UsuarioRepositorio>(usuarioRepositorio);

    var usuarioServico = serviceProvider.GetService<IUsuarioServico>();
    Assert.NotNull(usuarioServico);
    Assert.IsType<UsuarioServico>(usuarioServico);
  }

  [Fact]
  public void AddGestaoUsuarios_DeveLancarArgumentNullException_QuandoConnectionStringEstaNula()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddLogging();

    var configuration = Substitute.For<IConfiguration>();
    configuration.GetConnectionString("GestaoUsuarios").Returns((string?)null);

    // Act & Assert
    var exception = Assert.Throws<ArgumentNullException>(() => services.AddGestaoUsuarios(configuration));
    Assert.Matches("string de conexão", exception.Message);
  }
}
