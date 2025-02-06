using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.WebAPI.Controllers;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Controllers;

public class UsuariosControllerTestes
{
  private readonly IUsuarioServico _usuarioServico;
  private readonly UsuariosController _controller;

  public UsuariosControllerTestes()
  {
    _usuarioServico = Substitute.For<IUsuarioServico>();
    _controller = new UsuariosController(_usuarioServico);
  }

  [Fact]
  public async Task Criar_RetornaCreated_QuandoModelStateValidoAsync()
  {
    // Arrange
    _usuarioServico
      .CriarAsync(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(Task.FromResult(Resposta<Usuario>.ConcluidaComSucesso(new Usuario("Teste", DateTime.Now.Date.AddYears(-10), "teste@teste.com", "teste123"))));

    var criarUsuarioRecurso = new CriarUsuarioRecurso
    {
      Nome = "Usuário Teste",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    // Act
    var resultado = await _controller.CriarAsync(criarUsuarioRecurso, CancellationToken.None) as CreatedResult;

    // Assert
    Assert.NotNull(resultado);
    Assert.Equal(201, resultado.StatusCode);
    Assert.StartsWith("/api/usuarios/", resultado.Location);
  }

  [Fact]
  public async Task Criar_RetornaBadRequest_QuandoModelStateInvalidoAsync()
  {
    // Arrange
    var criarUsuarioRecurso = new CriarUsuarioRecurso
    {
      Nome = "", // Nome inválido (campo obrigatório).
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "invalid-email", // Email inválido.
      Senha = "Password123!"
    };

    _controller.ModelState.AddModelError("Nome", "O nome é obrigatório.");
    _controller.ModelState.AddModelError("Email", "O email informado não é válido.");

    // Act
    var result = await _controller.CriarAsync(criarUsuarioRecurso, CancellationToken.None) as BadRequestObjectResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(400, result.StatusCode);

    var erroRecurso = result.Value as ErroRecurso;
    Assert.NotNull(erroRecurso);
    Assert.Contains(erroRecurso.Mensagens, x => x.Contains("O nome é obrigatório."));
    Assert.Contains(erroRecurso.Mensagens, x => x.Contains("O email informado não é válido."));
  }

  [Fact]
  public async Task Criar_RetornaBadRequest_QuandoCriacaoUsuarioFalhaAsync()
  {
    // Arrange
    _usuarioServico
      .CriarAsync(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(Task.FromResult(Resposta<Usuario>.Erro("Ocorreu um erro ao gravar os dados.")));

    var criarUsuarioRecurso = new CriarUsuarioRecurso
    {
      Nome = "Usuário Teste",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    // Act
    var result = await _controller.CriarAsync(criarUsuarioRecurso, CancellationToken.None) as BadRequestObjectResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(400, result.StatusCode);

    var erroRecurso = result.Value as ErroRecurso;
    Assert.NotNull(erroRecurso);
    Assert.Contains(erroRecurso.Mensagens, x => x.Contains("Ocorreu um erro ao gravar os dados."));
  }
}
