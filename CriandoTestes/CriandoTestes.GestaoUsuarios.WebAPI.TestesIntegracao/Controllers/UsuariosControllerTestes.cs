using System.Net;
using System.Text;
using System.Text.Json;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Controllers;

public class UsuariosControllerTestes : TesteIntegracaoFixture
{
  public UsuariosControllerTestes(CustomWebApplicationFactory<Program> factory) : base(factory)
  {
  }

  [Fact]
  public async Task CriarAsync_RetornaCreatedAsync()
  {
    // Arrange
    var criarUsuarioRecurso = new CriarUsuarioRecurso
    {
      Nome = "Usuário Teste",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    var payload = new StringContent(JsonSerializer.Serialize(criarUsuarioRecurso), Encoding.UTF8, "application/json");

    // Act
    var response = await _client.PostAsync("/usuarios", payload);

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 201-299
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
  }

  [Fact]
  public async Task CriarAsync_RetornaBadRequest_QuandoModelStateInvalidoAsync()
  {
    // Arrange
    var criarUsuarioRecurso = new CriarUsuarioRecurso
    {
      Nome = "", // Nome inválido (campo obrigatório).
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "invalid-email", // Email inválido.
      Senha = "Password123!"
    };

    var content = new StringContent(JsonSerializer.Serialize(criarUsuarioRecurso), Encoding.UTF8, "application/json");

    // Act
    var response = await _client.PostAsync("/usuarios", content);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }
}
