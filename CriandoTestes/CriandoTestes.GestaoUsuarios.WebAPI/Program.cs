using CriandoTestes.GestaoUsuarios.Configuracoes.Extensoes;
using Microsoft.OpenApi.Models;

namespace CriandoTestes.GestaoUsuarios.WebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Dependências da API.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(cfg =>
    {
      cfg.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Criando Testes - Web API",
        Version = "v1",
        Description = "Exemplo de como criar testes unitários e de integração para uma Web API.",
      });

      cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CriandoTestes.GestaoUsuarios.WebAPI.xml"));
    });

    // Gestão de usuários
    builder.Services.AddGestaoUsuarios(builder.Configuration);

    var app = builder.Build();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.Run();
  }
}
