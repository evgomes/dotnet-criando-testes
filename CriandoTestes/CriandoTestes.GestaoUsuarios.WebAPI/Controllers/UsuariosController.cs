using System.Collections.Generic;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
  private readonly IUsuarioServico _usuarioServico;

  public UsuariosController(IUsuarioServico usuarioServico)
  {
    _usuarioServico = usuarioServico;
  }

  [HttpPost]
  public async Task<IActionResult> CriarAsync([FromBody] CriarUsuarioRecurso requisicao, CancellationToken cancellationToken)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(new ErroRecurso(GetMensagensErro(ModelState)));
    }

    var resposta = await _usuarioServico.CriarAsync(requisicao.Nome, requisicao.DataNascimento, requisicao.Email, requisicao.Senha, cancellationToken);
    if(!resposta.Sucesso)
    {
      return BadRequest(resposta.Mensagem!);
    }

    return Created($"/api/usuarios/{resposta.Dados!.Id}", null);
  }

  private static IEnumerable<string> GetMensagensErro(ModelStateDictionary modelState)
    => modelState.SelectMany(m => m.Value!.Errors).Select(m => m.ErrorMessage);
}
