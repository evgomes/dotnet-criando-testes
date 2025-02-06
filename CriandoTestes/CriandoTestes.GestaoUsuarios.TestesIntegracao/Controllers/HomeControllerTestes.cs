using System.Net;
using System.Net.Http.Json;
using CriandoTestes.GestaoUsuarios.Mvc.Models;
using CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace CriandoTestes.GestaoUsuarios.TestesIntegracao.Controllers;

public class HomeControllerTestes : MvcTesteIntegracaoFixture
{
  public HomeControllerTestes(CustomWebApplicationFactory<Mvc.Program> factory) : base(factory)
  {
  }

  [Fact]
  public async Task Home_RetornaViewComSucessoAsync()
  {
    // Act
    var response = await _client.GetAsync("/");

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 200-299
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var conteudo = await response.Content.ReadAsStringAsync();
    Assert.Contains("Criando Testes - Gestão de Usuários", conteudo);
  }

  [Fact]
  public async Task CriarUsuarioAsync_RetornaViewComSucessoAsync()
  {
    // Arrange
    var usuarioViewModel = new UsuarioViewModel
    {
      Nome = "Teste Usuario",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "teste@teste.com",
      Senha = "Password123!"
    };


    // Act
    var response = await _client.PostAsJsonAsync("/Home/CriarUsuario", usuarioViewModel);

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 200-299
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
