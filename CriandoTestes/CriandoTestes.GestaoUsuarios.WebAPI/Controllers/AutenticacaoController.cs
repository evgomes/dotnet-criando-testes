using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Controllers;

[ApiController]
[Route("/autenticacao")]
[AllowAnonymous]
public class AutenticacaoController : ControllerBase
{
  private const string Secret = "SecretExemploComVariosCaracteresSeguro";
  private const string Issuer = "CriandoTestes.WebAPI";
  private const string Audience = "CriandoTestes.WebAPI";

  [HttpPost]
  public IActionResult Autenticar([FromBody] AutenticarRecurso autenticarRecurso)
  {
    if(!ModelState.IsValid)
    {
      return BadRequest(new ErroRecurso(ModelState.GetMensagensErro()));
    }

    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, autenticarRecurso.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
    };

    var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    var credentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

    // 4. Criar o token
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddHours(1),
      Issuer = Issuer,
      Audience = Audience,
      SigningCredentials = credentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var securityToken = tokenHandler.CreateToken(tokenDescriptor);

    var token = tokenHandler.WriteToken(securityToken);
    return Ok(new TokenRecurso { Token = token });
  }
}
