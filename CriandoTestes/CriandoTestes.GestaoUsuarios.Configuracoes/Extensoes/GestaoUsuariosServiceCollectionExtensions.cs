using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using CriandoTestes.GestaoUsuarios.Dados.EFCore.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.Servicos.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CriandoTestes.GestaoUsuarios.Configuracoes.Extensoes;

public static class GestaoUsuariosServiceCollectionExtensions
{
  public static void AddGestaoUsuarios(this IServiceCollection services, IConfiguration configuration)
  {
    var stringConexao = configuration.GetConnectionString("GestaoUsuarios");
    if (string.IsNullOrWhiteSpace(stringConexao))
    {
      throw new ArgumentNullException("A string de conexão para gestão de usuários está vazia.");
    }

    services.AddDbContext<GestaoUsuariosContexto>(options =>
    {
      options.UseInMemoryDatabase("GestaoUsuarios");
      //options.UseNpgsql(stringConexao);
    });

    services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
    services.AddScoped<IUsuarioServico, UsuarioServico>();
  }
}
