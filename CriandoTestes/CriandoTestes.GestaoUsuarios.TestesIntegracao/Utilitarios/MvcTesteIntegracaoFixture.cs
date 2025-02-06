using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

public abstract class MvcTesteIntegracaoFixture : IClassFixture<CustomWebApplicationFactory<Mvc.Program>>, IAsyncLifetime
{
  protected readonly CustomWebApplicationFactory<Mvc.Program> _factory;
  protected readonly HttpClient _client;

  public MvcTesteIntegracaoFixture(CustomWebApplicationFactory<Mvc.Program> factory)
  {
    _factory = factory;
    _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
      AllowAutoRedirect = false, // Impede redirect automático.
      HandleCookies = true // Habilita manipulação de cookies.
    });
  }

  /// <summary>
  /// Método chamado antes da execução de cada teste que utiliza essa fixture.
  /// 
  /// Utilize para preparar dados e recursos conforme necessidade.
  /// </summary>
  /// <returns></returns>
  public virtual async Task InitializeAsync()
  {
    // Garante que a base de dados seja recriada em cada teste.
    using var scope = _factory.Services.CreateScope();
    var contexto = scope.ServiceProvider.GetRequiredService<GestaoUsuariosContexto>();
    await contexto.Database.EnsureCreatedAsync();
  }

  /// <summary>
  /// Método chamado após a execução de cada teste que utiliza essa fixture.
  /// 
  /// Utilize para limpar recursos conforme necessidade.
  /// </summary>
  /// <returns></returns>
  public virtual async Task DisposeAsync()
  {
    // Garante que os dados da base de dados sejam excluídos após a execução dos testes.
    using var scope = _factory.Services.CreateScope();
    var contexto = scope.ServiceProvider.GetRequiredService<GestaoUsuariosContexto>();
    await contexto.Database.EnsureDeletedAsync();
  }
}
