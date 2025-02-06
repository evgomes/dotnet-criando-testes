using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Controllers;

[ApiController]
[Route("dados-restritos")]
[Authorize]
public class DadosRestritosController : ControllerBase
{
  [HttpGet]
  public IActionResult GetDados()
  {
    return Ok(new List<string> { "Teste 1", "Teste 2", "Teste 3" });
  }
}
