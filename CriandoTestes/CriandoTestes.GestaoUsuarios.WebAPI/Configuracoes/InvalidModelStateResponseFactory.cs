using CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Mvc;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Configuracoes;

public static class InvalidModelStateResponseFactory
{
  public static IActionResult GerarRespostaErro(ActionContext context)
  {
    var erros = context.ModelState.GetMensagensErro();
    var response = new ErroRecurso(mensagens: erros);

    return new BadRequestObjectResult(response);
  }
}
