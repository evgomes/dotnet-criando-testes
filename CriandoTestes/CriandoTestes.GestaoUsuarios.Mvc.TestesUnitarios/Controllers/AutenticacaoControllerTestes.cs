using CriandoTestes.GestaoUsuarios.Mvc.Controllers;
using CriandoTestes.GestaoUsuarios.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CriandoTestes.GestaoUsuarios.Mvc.TestesUnitarios.Controllers;

public class AutenticacaoControllerTestes
{
  private readonly AutenticacaoController _controller;
  private readonly HttpContext _httpContext;
  private readonly IAuthenticationService _authenticationService;

  public AutenticacaoControllerTestes()
  {
    _httpContext = Substitute.For<HttpContext>();
    _authenticationService = Substitute.For<IAuthenticationService>();

    var serviceCollection = new ServiceCollection();
    serviceCollection.AddSingleton(_authenticationService);
    serviceCollection.AddSingleton(Substitute.For<IUrlHelperFactory>());
    var serviceProvider = serviceCollection.BuildServiceProvider();

    _httpContext.RequestServices.Returns(serviceProvider);

    _controller = new AutenticacaoController
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = _httpContext
      },
    };

    _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Substitute.For<ITempDataProvider>());
  }

  [Fact]
  public void Login_DeveRetornarView()
  {
    // Act
    var result = _controller.Login() as ViewResult;

    // Assert
    Assert.NotNull(result);
  }

  [Fact]
  public async Task LoginAsync_QuandoModelStateValido_DeveEfetuarLoginAsync()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "valid.email@example.com",
      Senha = "password123"
    };

    // Act
    var result = await _controller.LoginAsync(viewModel) as RedirectToActionResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Index", result?.ActionName);
    Assert.Equal("Home", result?.ControllerName);
  }

  [Fact]
  public async Task LoginAsync_QuandoModelStateInvalido_DeveRetornarMensagemErroAsync()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "invalid-email",
      Senha = "password123"
    };
    _controller.ModelState.AddModelError("Email", "O email informado não é válido.");

    // Act
    var result = await _controller.LoginAsync(viewModel) as RedirectToActionResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Home", result?.ControllerName);
    Assert.Equal("Index", result?.ActionName);
    Assert.False(_controller.ModelState.IsValid);
  }

  [Fact]
  public async Task LogoutAsync_DeveEfetuarLogoutAsync()
  {
    // Act
    var result = await _controller.LogoutAsync() as RedirectToActionResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Index", result?.ActionName);
    Assert.Equal("Home", result?.ControllerName);
  }
}
