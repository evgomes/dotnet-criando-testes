using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesIntegracao.Utilitarios;

/// <summary>
/// Factory para o host da aplicação web que substitui os serviços originais nas dependências por serviços
/// próprios para os testes de integração (exemplo: base de dados de testes).
/// </summary>
/// <typeparam name="TStartup">Tipo de classe que realiza o startup da aplicação, configurando dependências e pipeline.</typeparam>
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      ConfigurarContextosBaseDados(services);
    });

    return base.CreateHost(builder);
  }

  /// <summary>
  /// Método utilizado para configurar os contextos de base de dados para testes de interação.
  /// 
  /// A recomendação é utilizar uma base de dados real, com a mesma tecnologia (exemplo: PostgreSQL) para rodar
  /// os testes de integração.
  /// 
  /// Referência: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0#introduction-to-integration-tests
  /// </summary>
  /// <param name="services">Service collection.</param>
  private static void ConfigurarContextosBaseDados(IServiceCollection services)
  {
    // Remove o DbContext adicionado anteriormente.
    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GestaoUsuariosContexto>));
    if (descriptor != null)
    {
      services.Remove(descriptor);
    }

    // Reconfigura o DbContext para usar uma connection string de testes. 
    var configuracao = ConfigurationFactory.Build();
    services.AddDbContext<GestaoUsuariosContexto>(options =>
    {
      //options.UseNpgsql(configuracao.GetConnectionString("GestaoUsuariosTestesIntegracao"));
      options.UseInMemoryDatabase("GestaoUsuariosTestesIntegracao");
    });
  }
}
