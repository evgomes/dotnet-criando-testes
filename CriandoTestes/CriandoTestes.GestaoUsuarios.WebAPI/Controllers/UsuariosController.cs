using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Mvc;

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
      return BadRequest(new ErroRecurso(ModelState.GetMensagensErro()));
    }

    var resposta = await _usuarioServico.CriarAsync(requisicao.Nome, requisicao.DataNascimento!.Value, requisicao.Email, requisicao.Senha, cancellationToken);
    if (!resposta.Sucesso)
    {
      return BadRequest(new ErroRecurso(new string[] { resposta.Mensagem! }));
    }

    return Created($"/api/usuarios/{resposta.Dados!.Id}", null);
  }
}
