using System.Net;
using System.Net.Http.Json;
using CriandoTestes.GestaoUsuarios.Mvc.Models;
using CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

namespace CriandoTestes.GestaoUsuarios.TestesIntegracao.Controllers;

public class AutenticacaoControllerTestes : MvcTesteIntegracaoFixture
{
  public AutenticacaoControllerTestes(CustomWebApplicationFactory<Mvc.Program> factory) : base(factory)
  {
  }

  [Fact]
  public async Task Login_RetornaViewComSucessoAsync()
  {
    // Act
    var response = await _client.GetAsync("/Autenticacao/Login");

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 200-299
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var conteudo = await response.Content.ReadAsStringAsync();
    Assert.Contains("Login", conteudo);
  }

  [Fact]
  public async Task LoginAsync_Valido_RedirecionaParaHomeAsync()
  {
    // Arrange
    var autenticacaoViewModel = new AutenticacaoViewModel
    {
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    var formContent = new FormUrlEncodedContent(new[]
    {
      new KeyValuePair<string, string>("Email", autenticacaoViewModel.Email),
      new KeyValuePair<string, string>("Senha", autenticacaoViewModel.Senha)
    });

    // Act
    var response = await _client.PostAsync("/Autenticacao/Login", formContent);
    var content = await response.Content.ReadAsStringAsync();

    // Assert
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // 302 - Redirect
    Assert.Equal("/", response.Headers.Location?.ToString());
  }

  [Fact]
  public async Task LogoutAsync_RedirecionaParaHomeAsync()
  {
    // Act
    var response = await _client.PostAsync("/Autenticacao/Logout", null);

    // Assert
    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // 302 - Redirect
    Assert.Equal("/", response.Headers.Location?.ToString());
  }
}
