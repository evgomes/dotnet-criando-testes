using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using CriandoTestes.GestaoUsuarios.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CriandoTestes.GestaoUsuarios.Mvc.Controllers;

public class HomeController : Controller
{
  private readonly IUsuarioServico _usuarioServico;

  public HomeController(IUsuarioServico usuarioServico)
  {
    _usuarioServico = usuarioServico;
  }

  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> CriarUsuarioAsync(UsuarioViewModel model, CancellationToken cancellationToken)
  {
    if (ModelState.IsValid)
    {
      var resposta = await _usuarioServico.CriarAsync(model.Nome, model.DataNascimento!.Value, model.Email, model.Senha, cancellationToken);
      TempData["Sucesso"] = resposta.Sucesso;
      TempData["Mensagem"] = resposta.Mensagem ?? $"Usuário {model.Email} criado com sucesso.";

      return View(nameof(Index));
    }

    return View("Index", model);
  }
}
