using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.Mvc.Controllers;
using CriandoTestes.GestaoUsuarios.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using Xunit;

namespace CriandoTestes.GestaoUsuarios.Mvc.TestesUnitarios.Controllers;

public class HomeControllerTestes
{
  private readonly IUsuarioServico _usuarioServico;
  private readonly HomeController _controller;

  public HomeControllerTestes()
  {
    _usuarioServico = Substitute.For<IUsuarioServico>();
    _controller = new HomeController(_usuarioServico);
    _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
  }

  [Fact]
  public void Index_RetornaViewResult()
  {
    // Act
    var result = _controller.Index() as ViewResult;

    // Assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task CriarUsuarioAsync_RetornaViewResult_QuandoModelStateValidoAsync()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
      Senha = "password123"
    };

    _usuarioServico
      .CriarAsync(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(Task.FromResult(Resposta<Usuario>.ConcluidaComSucesso(new Usuario("John Doe", new DateTime(1990, 1, 1), "john.doe@example.com", "password123"))));

    // Act
    var result = await _controller.CriarUsuarioAsync(viewModel, CancellationToken.None) as ViewResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Index", result?.ViewName);
    Assert.True((bool)_controller.TempData["Sucesso"]);
    Assert.Equal($"Usuário {viewModel.Email} criado com sucesso.", _controller.TempData["Mensagem"]);
  }

  [Fact]
  public async Task CriarUsuarioAsync_RetornaViewResult_QuandoModelStateInvalidoAsync()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "invalid-email",
      Senha = "password123"
    };

    _controller.ModelState.AddModelError("Nome", "O nome é obrigatório.");
    _controller.ModelState.AddModelError("Email", "É necessário informar um email válido.");

    // Act
    var result = await _controller.CriarUsuarioAsync(viewModel, CancellationToken.None) as ViewResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Index", result?.ViewName);
    Assert.False(_controller.ModelState.IsValid);
  }
}
