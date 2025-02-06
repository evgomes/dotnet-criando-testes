using System.Text;
using CriandoTestes.GestaoUsuarios.Configuracoes.Extensoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Configuracoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CriandoTestes.GestaoUsuarios.WebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Dependências da API.
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
      options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.GerarRespostaErro;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(cfg =>
    {
      cfg.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Criando Testes - Web API",
        Version = "v1",
        Description = "Exemplo de como criar testes unitários e de integração para uma Web API.",
      });

      cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = $@"JSON Web Token para acessar recursos de API. Exemplo: <i>Bearer {{token}}</i>.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
      });

      cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
          },
          new [] { string.Empty }
        }
      });

      cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CriandoTestes.GestaoUsuarios.WebAPI.xml"));
    });

    builder
      .Services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = "CriandoTestes.WebAPI",
          ValidAudience = "CriandoTestes.WebAPI",
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretExemploComVariosCaracteresSeguro"))
      };
    });

    // Gestão de usuários
    builder.Services.AddGestaoUsuarios(builder.Configuration);

    var app = builder.Build();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.Run();
  }
}
