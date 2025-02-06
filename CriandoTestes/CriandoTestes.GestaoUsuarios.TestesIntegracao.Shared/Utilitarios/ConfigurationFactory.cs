using Microsoft.Extensions.Configuration;

namespace CriandoTestes.GestaoUsuarios.TestesIntegracao.Shared.Utilitarios;

/// <summary>
/// Factory utilizad para leitura de configurações provenientes de um arquivo "appsettings" e de variáveis de ambiente para os testes.
/// </summary>
public static class ConfigurationFactory
{
  public static IConfiguration Build()
  {
    var builder = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();

    return builder.Build();
  }
}
