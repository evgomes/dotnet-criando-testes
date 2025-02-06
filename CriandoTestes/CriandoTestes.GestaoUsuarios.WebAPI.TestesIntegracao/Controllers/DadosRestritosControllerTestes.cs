using System.Net;
using System.Text;
using System.Text.Json;
using CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Controllers;

public class DadosRestritosControllerTestes : TesteIntegracaoFixture
{
  public DadosRestritosControllerTestes(CustomWebApplicationFactory<Program> factory) : base(factory)
  {
  }

  [Fact]
  public async Task GetDados_RetornaUnauthorized_QuandoNaoAutenticadoAsync()
  {
    // Act
    var response = await _client.GetAsync("/dados-restritos");

    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Fact]
  public async Task GetDados_RetornaOk_QuandoAutenticadoAsync()
  {
    // Arrange
    var token = await ObterTokenValidoAsync();

    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    // Act
    var response = await _client.GetAsync("/dados-restritos");

    // Assert
    response.EnsureSuccessStatusCode(); // Status Code 200-299
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var responseContent = await response.Content.ReadAsStringAsync();
    var dados = JsonSerializer.Deserialize<List<string>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    Assert.NotNull(dados);
    Assert.Equal(3, dados.Count);
  }

  private async Task<string> ObterTokenValidoAsync()
  {
    var autenticarRecurso = new AutenticarRecurso
    {
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    var payload = new StringContent(JsonSerializer.Serialize(autenticarRecurso), Encoding.UTF8, "application/json");
    var response = await _client.PostAsync("/autenticacao", payload);

    response.EnsureSuccessStatusCode(); // Status Code 200-299

    var responseContent = await response.Content.ReadAsStringAsync();
    var tokenRecurso = JsonSerializer.Deserialize<TokenRecurso>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return tokenRecurso!.Token;
  }
}
