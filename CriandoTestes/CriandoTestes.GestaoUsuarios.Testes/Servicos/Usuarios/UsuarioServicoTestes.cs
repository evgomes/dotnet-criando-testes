using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Repositorios;
using CriandoTestes.GestaoUsuarios.Servicos.Usuarios;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CriandoTestes.GestaoUsuarios.Servicos.Tests.Usuarios;

public class UsuarioServicoTestes
{
  private readonly IUsuarioRepositorio _usuarioRepositorio;
  private readonly ILogger<UsuarioServico> _logger;
  private readonly UsuarioServico _usuarioServico;

  public UsuarioServicoTestes()
  {
    _usuarioRepositorio = Substitute.For<IUsuarioRepositorio>();
    _logger = Substitute.For<ILogger<UsuarioServico>>();
    _usuarioServico = new UsuarioServico(_usuarioRepositorio, _logger);
  }

  [Fact]
  public async Task CriarAsync_DeveRetornarErro_QuandoEmailJaExisteAsync()
  {
    // Arrange
    var email = "existente@example.com";
    _usuarioRepositorio.BuscarPorEmailAsync(email, Arg.Any<CancellationToken>()).Returns(new Usuario("Existente", new DateTime(1990, 1, 1), email, "password123"));

    // Act
    var resultado = await _usuarioServico.CriarAsync("Usuário Teste", DateTime.Now, email, "senha", CancellationToken.None);

    // Assert
    Assert.False(resultado.Sucesso);
    Assert.Matches("utilizado.", resultado.Mensagem);
  }

  [Fact]
  public async Task CriarAsync_DeveRetornarSucesso_QuandoUsuarioCriadoAsync()
  {
    // Arrange
    var email = "novousuario@example.com";
    _usuarioRepositorio.BuscarPorEmailAsync(email, Arg.Any<CancellationToken>()).Returns((Usuario?)null);

    // Act
    var resultado = await _usuarioServico.CriarAsync("Usuário Teste", DateTime.Now, email, "senha", CancellationToken.None);

    // Assert
    Assert.True(resultado.Sucesso);
    Assert.NotNull(resultado.Dados);
    Assert.Equal(email, resultado.Dados.Email);
  }

  [Fact]
  public async Task CriarAsync_DeveRegistrarErroERetornarErro_QuandoExcecaoLancadaAsync()
  {
    // Arrange
    var email = "novousuario@example.com";
    _usuarioRepositorio.BuscarPorEmailAsync(email, Arg.Any<CancellationToken>()).Returns((Usuario?)null);
    _usuarioRepositorio.When(x => x.CriarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>())).Do(x => { throw new Exception("Erro no banco de dados"); });

    // Act
    var resultado = await _usuarioServico.CriarAsync("Usuário Teste", DateTime.Now, email, "senha", CancellationToken.None);

    // Assert
    Assert.False(resultado.Sucesso);
    Assert.Equal("Falha ao criar o usuário.", resultado.Mensagem);

    // Testa a chamada do método de log.
    _logger.Received(1).LogError(Arg.Any<Exception>(), "Falha ao criar o usuário.");
  }
}
