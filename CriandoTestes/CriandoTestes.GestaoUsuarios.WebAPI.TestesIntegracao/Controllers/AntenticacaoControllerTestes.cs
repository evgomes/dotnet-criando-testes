using System.Net;
using System.Text;
using System.Text.Json;
using CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Controllers;

public class AutenticacaoControllerTestes : TesteIntegracaoFixture
{
  public AutenticacaoControllerTestes(CustomWebApplicationFactory<Program> factory) : base(factory)
  {
  }

  [Fact]
  public async Task Autenticar_RetornaOk_ComTokenValidoAsync()
  {
    // Arrange
    var autenticarRecurso = new AutenticarRecurso
    {
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    var payload = new StringContent(JsonSerializer.Serialize(autenticarRecurso), Encoding.UTF8, "application/json");

    // Act
    var response = await _client.PostAsync("/autenticacao", payload);

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 200-299
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var responseContent = await response.Content.ReadAsStringAsync();
    var tokenRecurso = JsonSerializer.Deserialize<TokenRecurso>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    Assert.NotNull(tokenRecurso);
    Assert.False(string.IsNullOrEmpty(tokenRecurso.Token));
  }
}
