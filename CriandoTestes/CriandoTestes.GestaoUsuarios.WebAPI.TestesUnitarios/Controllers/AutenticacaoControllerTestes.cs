using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using CriandoTestes.GestaoUsuarios.WebAPI.Controllers;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Controllers;

public class AutenticacaoControllerTestes
{
  private readonly AutenticacaoController _controller;

  public AutenticacaoControllerTestes()
  {
    _controller = new AutenticacaoController();
  }

  [Fact]
  public void Autenticar_RetornaOk_ComTokenValido()
  {
    // Arrange
    var autenticarRecurso = new AutenticarRecurso
    {
      Email = "teste@teste.com",
      Senha = "Password123!"
    };

    // Act
    var result = _controller.Autenticar(autenticarRecurso) as OkObjectResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(200, result.StatusCode);

    var tokenRecurso = result.Value as TokenRecurso;
    Assert.NotNull(tokenRecurso);
    Assert.False(string.IsNullOrEmpty(tokenRecurso.Token));

    // Valida o token
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes("SecretExemploComVariosCaracteresSeguro");
    tokenHandler.ValidateToken(tokenRecurso.Token, new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateIssuer = true,
      ValidIssuer = "CriandoTestes.WebAPI",
      ValidateAudience = true,
      ValidAudience = "CriandoTestes.WebAPI",
      ValidateLifetime = true,
      ClockSkew = TimeSpan.Zero
    }, out SecurityToken validatedToken);

    Assert.NotNull(validatedToken);
  }

  [Fact]
  public void Autenticar_RetornaBadRequest_QuandoModelStateInvalido()
  {
    // Arrange
    var autenticarRecurso = new AutenticarRecurso
    {
      Email = "inválido", // Email inválido.
      Senha = "Password123!"
    };

    _controller.ModelState.AddModelError("Email", "O email informado não é válido.");

    // Act
    var result = _controller.Autenticar(autenticarRecurso) as BadRequestObjectResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(400, result.StatusCode);

    var erroRecurso = result.Value as ErroRecurso;
    Assert.NotNull(erroRecurso);
    Assert.Contains(erroRecurso.Mensagens, x => Regex.IsMatch(x, "email"));
  }
}
